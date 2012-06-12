// 
// GetMagicPreviewResponse.cs
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
    public class GetMagicPreviewResponse : Response
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private DateTime created = DateTime.MinValue;
        public DateTime Created
        {
            get { return this.created; }
        }
        
        private string md5 = String.Empty;
        public string Md5
        {
            get { return this.md5; }
        }
        
        private int size = -1;
        public int Size
        {
            get { return this.size; }
        }
        
        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
        }
        
        private string resume = String.Empty;
        public string Resume
        {
            get { return this.resume; }
        }
        
        public GetMagicPreviewResponse(string xml)
            : base(xml)
        {
        }
        
        protected override void ParseResponse(XmlDocument xml)
        {
            XmlNode responseNode = xml.SelectSingleNode("/response");
            
            if (responseNode.Attributes["id"] != null)
            {
                Int32.TryParse(responseNode.Attributes["id"].Value, out this.id);   
            }
            if (responseNode.Attributes["created"] != null)
            {
                DateTime.TryParse(responseNode.Attributes["created"].Value, out this.created);
            }
            if (responseNode.Attributes["md5"] != null)
            {
                this.md5 = responseNode.Attributes["md5"].Value;
            }
            if (responseNode.Attributes["size"] != null)
            {
                Int32.TryParse(responseNode.Attributes["size"].Value, out this.size);
            }
            if (responseNode.Attributes["name"] != null)
            {
                this.name = responseNode.Attributes["name"].Value;
            }
            
            XmlNode item = xml.SelectSingleNode("/response/item");
            if (item != null)
            {
                this.resume = item.InnerText;
            }
        }
    }
}

