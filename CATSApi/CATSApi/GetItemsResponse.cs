// 
// GetItemsResponse.cs
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
    public abstract class GetItemsResponse : Response
    {
        private int numPages = -1;
        public int NumPages
        {
            get { return this.numPages; }
        }
        
        private int rowsPerPage = -1;
        public int RowsPerPage
        {
            get { return this.rowsPerPage; }
        }
        
        private string search = String.Empty;
        public string Search
        {
            get { return this.search; }
        }
        
        private int pageNumber = -1;
        public int PageNumber
        {
            get { return this.pageNumber; }
        }
        
        private string sortColumn = String.Empty;
        public string SortColumn
        {
            get { return this.sortColumn; }
        }
        
        private CATSApi.SortDirection sortDir;
        public CATSApi.SortDirection SortDir
        {
            get { return this.sortDir; }
        }
        
        public GetItemsResponse(string xml)
            : base(xml)
        {
        }
        
        protected override void ParseResponse(XmlDocument xml)
        {
            XmlNode responseNode = xml.SelectSingleNode("/response");
            
            if (responseNode.Attributes["num_pages"] != null)
            {
                this.numPages = Convert.ToInt32(responseNode.Attributes["num_pages"].Value);
            }
            if (responseNode.Attributes["rows_per_page"] != null)
            {
                this.rowsPerPage = Convert.ToInt32(responseNode.Attributes["rows_per_page"].Value);
            }
            if (responseNode.Attributes["search"] != null)
            {
                this.search = responseNode.Attributes["search"].Value;
            }
            if (responseNode.Attributes["page_number"] != null)
            {
                this.pageNumber = Convert.ToInt32(responseNode.Attributes["page_number"].Value);
            }
            if (responseNode.Attributes["sort_column"] != null)
            {
                this.sortColumn = responseNode.Attributes["sort_column"].Value;
            }
            if (responseNode.Attributes["sort_dir"] != null)
            {
                switch (responseNode.Attributes["sort_dir"].Value)
                {
                    case "desc":
                        this.sortDir = CATSApi.SortDirection.Desc;
                        break;
                    case "asc":
                        this.sortDir = CATSApi.SortDirection.Asc;
                        break;
                }
            }
            
            XmlNodeList items = xml.GetElementsByTagName("item");
            foreach (XmlNode item in items)
            {
                this.ParseItem(item);
            }
            
            base.ParseResponse(xml);
        }
        
        protected virtual void ParseItem(XmlNode item)
        {   
        }
    }
}

