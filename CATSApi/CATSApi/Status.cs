// 
// Status.cs
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
    public class Status
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        private int position = -1;
        public int Position
        {
            get { return this.position; }
        }
        
        private int requires = -1;
        public int Requires
        {
            get { return this.requires; }
        }
        
        private string backgroundColor = String.Empty;
        public string BackgroundColor
        {
            get { return this.backgroundColor; }
        }
        
        private string foregroundColor = String.Empty;
        public string ForegroundColor
        {
            get { return this.foregroundColor; }
        }
        
        private int statusMappingId = -1;
        public int StatusMappingId
        {
            get { return this.statusMappingId; }
        }
        
        private string statusMapping = String.Empty;
        public string StatusMapping
        {
            get { return this.statusMapping; }
        }
        
        public Status(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("id").InnerText, out this.id);
            this.title = xml.SelectSingleNode("title").InnerText;
            Int32.TryParse(xml.SelectSingleNode("position").InnerText, out this.position);
            Int32.TryParse(xml.SelectSingleNode("requires").InnerText, out this.requires);
            this.backgroundColor = xml.SelectSingleNode("background_color").InnerText;
            this.foregroundColor = xml.SelectSingleNode("foreground_color").InnerText;
            Int32.TryParse(xml.SelectSingleNode("status_mapping_id").InnerText, out this.statusMappingId);
            this.statusMapping = xml.SelectSingleNode("status_mapping").InnerText;
        }
    }
}

