// 
// Response.cs
//  
// Author:
//       Graham Floyd <gfloyd@catsone.com>
// 
// Copyright (c) 2012 CATS Software, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Xml;

namespace CATS
{
    public abstract class Response
    {
        protected bool success;
        public bool Success
        {
            get { return this.success; }            
        }

        private int numResults;
        public int NumResults
        {
            get { return this.numResults; }
        }
        
        private Error error;
        public Error Error
        {
            get { return (Error)this.error; }
        }
        
        public Response(string xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            XmlNode responseNode = xmlDocument.SelectSingleNode("/response");
            this.success = Convert.ToBoolean(responseNode.Attributes["success"].Value);         

            if (responseNode.Attributes["num_results"] != null)
            {
                this.numResults = Convert.ToInt32(responseNode.Attributes["num_results"].Value);
            }                       
            
            if (this.success)
            {
                ParseResponse(xmlDocument);
            }
            else
            {
                this.error = new Error(responseNode.SelectSingleNode("error"));
            }
        }

        protected virtual void ParseResponse(XmlDocument xml)
        {
        }
    }
}

