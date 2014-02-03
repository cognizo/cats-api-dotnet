﻿ // 
// GetUserLanguageResponse.cs
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
using System.Collections.Generic;
using System.Xml;

namespace CATS
{
    public class GetUserLanguageResponse : Response
    {
        private string language = String.Empty;
        public string Language
        {
            get { return this.language; }
        }

        private DateTime lastModified = DateTime.MinValue;
        public DateTime LastModified
        {
            get { return this.lastModified; }
        }

        public GetUserLanguageResponse(string xml)
            : base(xml)
        {
        }

        protected override void ParseResponse(XmlDocument xml)
        {
            XmlNode responseNode = xml.SelectSingleNode("/response");

            this.language = responseNode.SelectSingleNode("language").InnerText;
            DateTime.TryParse(responseNode.SelectSingleNode("last_modified").InnerText, out this.lastModified);

            base.ParseResponse(xml);
        }
    }
}

