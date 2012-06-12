// 
// Main.cs
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
using CATS;

namespace CATSApiExample
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CATSApi catsApi = new CATSApi("catsone.com", "subdomain", "<Transaction Code>");

            // Add a joborder
            AddJoborderRequest addJoborderRequest = new AddJoborderRequest();
            addJoborderRequest.Title = "Marketing Specialist";
            addJoborderRequest.City = "Minneapolis";
            addJoborderRequest.State = "MN";            
            AddJoborderResponse addJoborderResponse = catsApi.AddJoborder(addJoborderRequest);
            Console.WriteLine(addJoborderResponse.Id);
            
            // Update joborder
            UpdateJoborderRequest updateJoborderRequest = new UpdateJoborderRequest();
            updateJoborderRequest.Id = addJoborderResponse.Id;
            updateJoborderRequest.City = "Saint Paul";
            UpdateJoborderResponse updateJoborderResponse = catsApi.UpdateJoborder(updateJoborderRequest);
            Console.WriteLine(updateJoborderResponse.Id);
            
            // Get joborder
            GetJoborderResponse getJoborderResponse = catsApi.GetJoborder(addJoborderResponse.Id);
            Console.WriteLine(getJoborderResponse.City);
            
            // Delete joborder
            DeleteJoborderResponse deleteJoborderResponse = catsApi.DeleteJoborder(addJoborderResponse.Id);
            Console.WriteLine(deleteJoborderResponse.NumResults);
            
            // Error handling
            AddCompanyRequest addCompanyRequest = new AddCompanyRequest();
            addCompanyRequest.Address = "123 Imaginary Street";
            AddCompanyResponse addCompanyResponse = catsApi.AddCompany(addCompanyRequest);
            if (!addCompanyResponse.Success)
            {
                Console.WriteLine(addCompanyResponse.Error.Message);
            }
        }
    }
}
