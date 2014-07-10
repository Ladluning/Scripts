using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace NetMobile
{
	public class CSharpSocketTalkInterface :NetBase
	{
		protected Socket clientSocket;
		protected IPAddress ip;
		protected IPEndPoint connectAddress;
		private byte[] byteMessage;
		private bool isSocketDisconnect = false;
		private Thread sendThread;
		private Thread receiveThread;

		public void Close()
		{	
			if(sendThread!=null)
				sendThread.Abort();
			if(receiveThread!=null)
				receiveThread.Abort();		
			//StopCoroutine("TalkSendMsg");
			//StopCoroutine("TalkReceiveMsg");
			StopConnect();
		}
		
		public bool InitSocket(string ipAddress,int port,int MaxMessageLength)
		{
			byteMessage = new byte[MaxMessageLength];
			
			ip = IPAddress.Parse(ipAddress);
			
			connectAddress = new IPEndPoint(ip,port);
			
			StartSendThread();
			
			if(!ConnectSocket())
			{
				return false;
			}

			return true;
		}
		
		public bool ConnectSocket()//
		{
			if(IsInConnect)
				return false;
			IsInConnect = true;
			//LogManager.Log ("Start Connected ");
			try{
				clientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
				clientSocket.Connect(connectAddress);
				isSocketDisconnect = true;
				LogManager.Log("Talk_Connect Server Success");

                this.SendEvent(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN, SendCommand.NewCommand(GameEvent.WebEvent.EVENT_WEB_SEND_LOGIN));

				IsInConnect = false;
				return true;
			}
			catch
			{
				//LogManager.Log("Talk_Connect Server Error Please Try");
				isSocketDisconnect = false;
				IsInConnect = false;
				return false;
			}		
		}
		
		private void StartSendThread()
		{
			//StartCoroutine(TalkSendMsg());
			//StartCoroutine(TalkReceiveMsg());
			sendThread = new Thread(new ThreadStart(TalkSendMsg));
			sendThread.Start();
			
			receiveThread = new Thread(new ThreadStart(TalkReceiveMsg));
			receiveThread.Start();
		}

		
		private bool CheckSocketConnect()
		{
			if(clientSocket==null||(clientSocket!=null&&!clientSocket.Connected))
			{
				//LogManager.Log("Talk Socket Is disconnects");
				StopConnect();
				ConnectSocket();
				return false;
			}
			return true;
		}
		
		private void TalkReceiveMsg()
		{
			while(true)
			{
				if(!CheckSocketConnect())
				{	
					Thread.Sleep(2000);
					continue;
				}
				
				try
				{
					//Debug.Log ("StartReceive");
					int byteMessageLength = clientSocket.Receive(byteMessage);
					byte[] RealMessage = new byte[byteMessageLength];
					Array.Copy(byteMessage,0,RealMessage,0,byteMessageLength);
					Msg.AddRange(RealMessage);
					
					//Debug.Log (Msg.Count+" "+byteMessageLength+" "+System.BitConverter.ToInt32(Msg.ToArray(),0));
					while(Msg.Count>0&&System.BitConverter.ToInt32(Msg.ToArray(),0)<=Msg.Count)
					{

						//Debug.Log ("Msg Lose:"+Msg.Count+" Current Message Length:"+System.BitConverter.ToInt32(Msg.ToArray(),0));
						int ReceiveCount = System.BitConverter.ToInt32(Msg.ToArray(),0);
						//Debug.Log (Encoding.Default.GetString(Msg.ToArray(),4,ReceiveCount-4));
						string tmpReceive = Encoding.Default.GetString(Msg.ToArray(),4,ReceiveCount-4);
						//Debug.Log (Msg.Count+" "+m_pPostQuene);
						NetPostQueue.receiveList.Add(tmpReceive);	
						//Debug.Log (Msg.Count);
						LogManager.Log("Logic_RECEIVE: "+tmpReceive); 
						Msg.RemoveRange(0,ReceiveCount);
						//Debug.Log (Msg.Count);
						//if(Msg.Count>0)
						//	Debug.Log (System.BitConverter.ToString(Msg.ToArray())+"Msg Lose:"+Msg.Count+" Current Message Length:"+System.BitConverter.ToInt32(Msg.ToArray(),0));
					}
					continue;
				}
				catch(SocketException er)
				{
					//StopConnect();
					LogManager.Log("Talk_Receive Error: "+er.ToString());
					continue;
				}
				Thread.Sleep(500);
			}
		}
		
		private void TalkSendMsg()
		{
			while(true)
			{
				if(!CheckSocketConnect())
				{	
					Thread.Sleep(2000);
					continue;
				}

				PostStruct sendStruct = GetTopMessage();

				if(sendStruct == null)
				{	
					//Debug.Log ("NoneSend");
					Thread.Sleep(500);
					continue;
				}

				Debug.Log (sendStruct);
				try{
					byte[]  Bytes2 = Encoding.ASCII.GetBytes(sendStruct.m_pSenderString);
					List<byte> tmp = new List<byte>(4+Bytes2.Length);
					tmp.AddRange(System.BitConverter.GetBytes(4+Bytes2.Length));
					tmp.AddRange(Bytes2);
					clientSocket.Send(tmp.ToArray());  
					LogManager.Log("Talk_SEND：" + sendStruct.m_pSenderString); 
				}
				catch(SocketException er){
					LogManager.Log("Talk_Send Error:"+er.ToString());
					//StopConnect();
					//Thread.Sleep(500);
					continue;
				}
			
				PullMessage(sendStruct);
				Thread.Sleep(500);
			}
		}
		
		private void StopConnect()
		{
			if(!isSocketDisconnect)
				return;
			
			clientSocket.Shutdown(SocketShutdown.Both);
			clientSocket.Close();
			isSocketDisconnect = false;
		}
	}
}

