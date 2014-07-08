using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

    public enum JsonType
    {
        None,

        Object,
        Array,
        String,
        Int,
        Long,
        Double,
        Boolean,

        Count
    }
    public class JsonData
    {
        #region Constructors


        private object m_value;
	
        public JsonData ()
        {
            m_value = null;
        }

        public JsonData ( object value )
        {
            m_value = value;
        }

        #endregion

        #region Indexer

        public JsonData this[string key]
        {
            get
            {
                if ( m_value == null )
                {
                    return null;
                }

                if ( m_value.GetType() == typeof( Dictionary<string, object> ) )
                {
                    return new JsonData( ((Dictionary<string,object>)m_value)[key] );
                }

                Debug.Log( "JsonData this[string key]"+m_value+" "+ ((Dictionary<string,object>)m_value)[key] +" "+key+"Error! Type = " + m_value.GetType().Name );
                return null;
            }
        }

        public JsonData this[int index]
        {
            get
            {
                if ( m_value.GetType() == typeof( List<object> ) )
                {
                    return new JsonData( ((List<object>)m_value)[index] );
                }
                else if ( m_value.GetType() == typeof( Dictionary<string, object> ) )
                {
                    int count = 0;
                    foreach ( object obj in (Dictionary<string,object>)m_value )
                    {
                        if ( count == index )
                        {
                            return new JsonData( ((DictionaryEntry)obj).Value );
                        }

                        count++;
                    }
                }
                else
                {
                    return new JsonData( m_value );
                }

                Debug.Log("JsonData this[int index]"+ " Error! Type = " + m_value.GetType().Name );
                return null;
            }
        }

        #endregion

        #region Explicite Cast

        public static explicit operator String ( JsonData data )
        {
            if ( data.m_value != null )
                return data.m_value.ToString();

            Debug.Log( "Not Containing a String" );
            return "";
        }

        public static explicit operator Int32 ( JsonData data )
        {
            int output = 0;
            if ( data.m_value != null && System.Int32.TryParse( data.m_value.ToString(), out output ) )
                return output;

            Debug.Log( "Not Containing a Int32" );
            return output;
        }

        public static explicit operator Int64 ( JsonData data )
        {
            long output = 0;
            if ( data.m_value != null && System.Int64.TryParse( data.m_value.ToString(), out output ) )
                return output;

            Debug.Log( "Not Containing a Int64" );
            return output;
        }

        public static explicit operator Boolean ( JsonData data )
        {
            bool output = false;
            if ( data.m_value != null && System.Boolean.TryParse( data.m_value.ToString(), out output ) )
                return output;

            Debug.Log( "Not Containing a Boolean" );
            return output;
        }

        public static explicit operator Double ( JsonData data )
        {
            double output = 0;
            if ( data.m_value != null && System.Double.TryParse( data.m_value.ToString(), out output ) )
                return output;

            Debug.Log( "Not Containing a Double" );
            return output;
        }

        #endregion

        public int Count
        {
            get
            {
                if ( m_value.GetType() == typeof( Dictionary<string, object> ) )
                    return ((Dictionary<string, object>)m_value).Count;
                else if ( m_value.GetType() == typeof( List<object> ) )
                    return ((List<object>)m_value).Count;


                return 1;
            }
        }

        public override string ToString ()
        {
            if ( m_value == null ) return "";

            return m_value.ToString();
        }

        public Dictionary<string,object> Dictionary
        {
            get
            {
                if ( m_value.GetType() == typeof( Dictionary<string, object> ) )
                {
                    return (Dictionary<string,object>)m_value;
                }

                return null;
            }
        }
	
		public bool CheckValueIsNull ()
		{
			if (m_value == null)
				return true;
			return false;
		}
	
    }

