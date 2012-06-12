// 
// AddAttachmentResponse.cs
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
    public class AddAttachmentResponse : Response
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }

        private string guid = String.Empty;
        public string Guid
        {
            get { return this.guid; }
        }

        private string md5 = String.Empty;
        public string Md5
        {
            get { return this.md5; }
        }

        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
        }

        private DateTime created = new DateTime();
        public DateTime Created
        {
            get { return this.created; }
        }

        public AddAttachmentResponse(string xml)
            : base(xml)
        {
        }

        protected override void ParseResponse(XmlDocument xml)
        {
            this.id = Convert.ToInt32(xml.SelectSingleNode("/response/id").InnerText);
            this.guid = xml.SelectSingleNode("/response/guid").InnerText;                
            this.md5 = xml.SelectSingleNode("/response/md5").InnerText;
            this.name = xml.SelectSingleNode("/response/name").InnerText;
            this.created = DateTime.Parse(xml.SelectSingleNode("/response/created").InnerText);

            base.ParseResponse(xml);
        }
    }
}

