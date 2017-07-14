﻿#region License

/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

#endregion

using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gremlin.Net.Structure.IO.GraphSON
{
    internal class ListSerializer : IGraphSONDeserializer, IGraphSONSerializer
    {
        private static readonly IReadOnlyList<object> EmptyList = new object[0];
        
        public dynamic Objectify(JToken graphsonObject, GraphSONReader reader)
        {
            var jArray = graphsonObject as JArray;
            if (jArray == null)
            {
                return EmptyList;
            }
            var result = new object[jArray.Count];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = reader.ToObject(jArray[i]);
            }
            // object[] implements IList<object>
            return result;
        }

        public Dictionary<string, dynamic> Dictify(dynamic objectData, GraphSONWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}