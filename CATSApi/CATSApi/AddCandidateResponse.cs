// 
// AddCandidateResponse.cs
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
    public class AddCandidateResponse : Response
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }

        private string firstName = String.Empty;
        public string FirstName
        {
            get { return this.firstName; }
        }

        private string lastName = String.Empty;
        public string LastName
        {
            get { return this.lastName; }
        }

        private int attachmentId = -1;
        public int AttachmentId
        {
            get { return this.attachmentId; }
        }

        private string attachmentGuid = String.Empty;
        public string AttachmentGuid
        {
            get { return this.attachmentGuid; }
        }

        private string attachmentFileName = String.Empty;
        public string AttachmentFileName
        {
            get { return this.attachmentFileName; }
        }

        public AddCandidateResponse(string xml)
            : base(xml)
        {
        }
        
        protected override void ParseResponse(XmlDocument xml)
        {
            Int32.TryParse(xml.SelectSingleNode("/response/id").InnerText, out this.id);
            this.firstName = xml.SelectSingleNode("/response/first_name").InnerText;
            this.lastName = xml.SelectSingleNode("/response/last_name").InnerText;
            Int32.TryParse(xml.SelectSingleNode("/response/attachment_id").InnerText, out this.attachmentId);
            this.attachmentGuid = xml.SelectSingleNode("/response/attachment_guid").InnerText;
            this.attachmentFileName = xml.SelectSingleNode("/response/attachment_file_name").InnerText;

            base.ParseResponse(xml);
        }       
    }
}

