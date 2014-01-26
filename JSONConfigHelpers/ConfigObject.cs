using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace JSONConfigHelpers
{
    public class ConfigObject
    {
        /// <summary>
        /// Parse a field from the json. Returns null if not found.
        /// </summary>
        /// <typeparam name="T">Expected Type</typeparam>
        /// <param name="j">JSON Object</param>
        /// <param name="field">Field name</param>
        /// <returns>The field as type T</returns>
        protected static T ParseJsonOrDefault<T>(JObject j, String field)
            where T : class
        {
            //Get field
            JToken ret;
            try
            {
                ret = j[field];
            }
            catch (Exception)
            {
                throw new Exception("Could not find data of the field " + field);
            }


            //Convert type
            try
            {
                return (T) Convert.ChangeType(ret, typeof (T));
            }
            catch (Exception)
            {
                try
                {
                    var attempt2 = (string) ret;
                    return (T) Convert.ChangeType(attempt2, typeof (T));
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Parse JSON object as Dictionary of strings
        /// </summary>
        /// <param name="j">json object</param>
        /// <param name="field">field name</param>
        /// <returns></returns>
        protected static Dictionary<String, String> ParseJsonDictionaryOrDefault(JObject j, String field)
        {
            //Get field
            JToken ret;
            try
            {
                ret = j[field];
            }
            catch (Exception)
            {
                throw new Exception("Could not find data of the field " + field);
            }


            var jObject = ret as JObject;
            if (jObject == null)
                return null;

            IDictionary<string, JToken> rates = jObject;

            return rates.ToDictionary(pair => pair.Key,
                                      pair => (String) pair.Value);
        }

        /// <summary>
        /// Parse a field from the json. Throws exception of field not found.
        /// </summary>
        /// <typeparam name="T">Expected Type</typeparam>
        /// <param name="j">JSON Object</param>
        /// <param name="field">Field name</param>
        /// <returns>The field as type T</returns>
        protected static T ParseJson<T>(JObject j, String field)
        {
            //Get field
            JToken ret;
            try
            {
                ret = j[field];
            }
            catch (Exception)
            {
                throw new Exception("Could not find data of the field " + field);
            }


            //Convert type
            try
            {
                return (T) Convert.ChangeType(ret, typeof (T));
            }
            catch (Exception)
            {
                try
                {
                    var attempt2 = (string) ret;
                    return (T) Convert.ChangeType(attempt2, typeof (T));
                }
                catch (Exception)
                {
                    throw new Exception("Failed to convert JSON type to " + typeof (T).Name);
                }
            }
        }
    }
}