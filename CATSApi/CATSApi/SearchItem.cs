// 
// SearchItem.cs
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
    public class SearchItem
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private CATSApi.ItemDataType? dataType = null;
        public CATSApi.ItemDataType DataType
        {
            get { return (CATSApi.ItemDataType)this.dataType; }
        }
        
        private string summary = String.Empty;
        public string Summary
        {
            get { return this.summary; }
        }
        
        public SearchItem(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("id").InnerText, out this.id);
            string itemDataType = xml.SelectSingleNode("data_type").InnerText;
            this.dataType = (CATSApi.ItemDataType)Enum.Parse(
                typeof(CATSApi.ItemDataType), 
                Char.ToUpper(itemDataType[0]) + itemDataType.Substring(1)
            );
            this.summary = xml.SelectSingleNode("summary").InnerText;
        }
    }
}

