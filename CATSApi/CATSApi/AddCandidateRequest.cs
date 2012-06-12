// 
// AddCandidateRequest.cs
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

namespace CATS
{
    public class AddCandidateRequest
    {
        public string FirstName = String.Empty;
        public string MiddleName = String.Empty;
        public string LastName = String.Empty;
        public string Title = String.Empty;
        public string PhoneHome = String.Empty;
        public string PhoneCell = String.Empty;
        public string PhoneWork = String.Empty;
        public string Address = String.Empty;
        public string City = String.Empty;
        public string State = String.Empty;
        public string Zip = String.Empty;
        public string Source = String.Empty;
        public DateTime DateAvailable = DateTime.MinValue;
        public bool CanRelocate = false;
        public string Notes = String.Empty;
        public string KeySkills = String.Empty;
        public string CurrentEmployer = String.Empty;
        public DateTime DateCreated = DateTime.MinValue;
        public DateTime DateModified = DateTime.MinValue;
        public int Owner = -1;
        public int EnteredBy = -1;
        public string Email1 = String.Empty;
        public string Email2 = String.Empty;
        public string WebSite = String.Empty;
        public bool IsHot = false;
        public string DesiredPay = String.Empty;
        public string CurrentPay = String.Empty;
        public bool IsActive = true;
        public string BestTimeToCall = String.Empty;
        public string Password = String.Empty;
        public int Country = -1;
        public string Resume = String.Empty;
        public bool ParseResume = false;
        public CATSApi.OnDuplicateAction OnDuplicate = CATSApi.OnDuplicateAction.Error;
        
        public AddCandidateRequest()
        {           
        }
    }
}

