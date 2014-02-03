// 
// CATSApi.cs
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
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace CATS
{
    public class CATSApi
    {
        public enum ActivityDataType
        {
            Candidate,
            Contact
        }
        
        public enum AttachmentDataType
        {
            Candidate,
            Contact,
            Joborder
        }

        public enum QuickNoteDataType
        {
            Company,
            Joborder
        }

        public enum ItemDataType
        {
            Candidate,
            Contact,
            Company,
            Joborder
        }
        
        public enum JoborderType
        {
            Contract,
            Hire,
            ContractToHire,
            Freelance
        }

        public enum ListType
        {
            Static,
            Dynamic
        }

        public enum ActivityType
        {
            Call,
            Email,
            Meeting,
            CallTalked,
            CallLvm,
            CallMissed,
            Other
        }

        public enum OnDuplicateAction
        {
            Ignore,
            Update,
            Error
        }
        
        public enum SortDirection
        {
            Desc,
            Asc
        }

        public string Domain { get; set; }

        public string Subdomain { get; set; }

        public string TransactionCode { get; set; }

        public CATSApi(string domain, string subdomain, string transactionCode)
        {
            this.Domain = domain;
            this.Subdomain = subdomain;
            this.TransactionCode = transactionCode;
        }

        public string MakeRequest(string function, Dictionary<string, string> data = null, Dictionary<string, string> files = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                "https://" + this.Subdomain + "." + this.Domain + "/api/" + function
            );

            request.Method = "POST";

            if (data == null)
            {
                data = new Dictionary<string, string>();
            }
            
            if (this.TransactionCode.Length > 0)
            {
                data["transaction_code"] = this.TransactionCode;
            }            

            if (files != null)
            {
                Stream requestStream = request.GetRequestStream();
                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;

                string formDataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

                requestStream = request.GetRequestStream();

                foreach (KeyValuePair<string, string> entry in data)
                {
                    string formItem = string.Format(formDataTemplate, entry.Key, entry.Value);
                    byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(formItem);
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length);
                }

                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

                int counter = 1;
                foreach (KeyValuePair<string, string> entry in files)
                {
                    string header = "";
                    if (files.Count == 1)
                    {
                        header = string.Format(headerTemplate, entry.Key, entry.Value);
                    }

                    byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
                    requestStream.Write(headerBytes, 0, headerBytes.Length);

                    FileStream fileStream = new FileStream(entry.Value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    byte[] buffer = new byte[4096];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                    fileStream.Close();

                    counter++;
                }

                requestStream.Close();
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";

                string post = BuildQueryString(data);
                request.ContentLength = post.Length;

                StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(post);
                streamWriter.Close();
            }

            string result = "";
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        private string BuildQueryString(Dictionary<string, string> data)
        {
            var values = HttpUtility.ParseQueryString(String.Empty);

            foreach (KeyValuePair<string, string> entry in data)
            {
                values[entry.Key] = entry.Value;
            }
            
            return values.ToString();
        }
        
        private string CamelCaseToUnderscore(string camelCase)
        {
            return Regex.Replace(camelCase, @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])", "$1$3_$2$4").ToLower();
        }
        
        private string DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds.ToString(); 
        }

        public static bool StringToBool(string value)
        {
            bool boolValue = false;
            
            switch (value.Trim().ToLower())
            {
                case "true":
                case "yes":
                case "1":
                    boolValue = true;
                    break;
            }
            
            return boolValue;
        }
        
        public AddActivityResponse AddActivity(ActivityDataType dataType, int id, string notes, 
            ActivityType activityType = ActivityType.Other, int joborderId = -1)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(ActivityDataType), dataType));
            data["id"] = id.ToString();
            data["type"] = CamelCaseToUnderscore(Enum.GetName(typeof(ActivityType), activityType));
            data["notes"] = notes;
            if (joborderId != -1)
            {
                data["joborder_id"] = joborderId.ToString();            
            }           
            
            AddActivityResponse response = new AddActivityResponse(MakeRequest("add_activity", data));
            return response;
        }

        public AddAttachmentResponse AddAttachment(AttachmentDataType dataType, int id, string file, 
            bool isResume = false)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(AttachmentDataType), dataType));
            data["id"] = id.ToString();

            Dictionary<string, string> files = new Dictionary<string, string>();
            files.Add("file", file);

            data["is_resume"] = isResume.ToString().ToLower();

            AddAttachmentResponse response = new AddAttachmentResponse(
                MakeRequest("add_attachment", data, files)
            );
            return response;
        }

        public AddCandidateResponse AddCandidate(AddCandidateRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();         
            data["first_name"] = request.FirstName;
            data["middle_name"] = request.MiddleName;
            data["last_name"] = request.LastName;
            data["title"] = request.Title;
            data["phone_home"] = request.PhoneHome;
            data["phone_cell"] = request.PhoneCell;
            data["phone_work"] = request.PhoneWork;
            data["address"] = request.Address;
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            data["source"] = request.Source;
            if (request.DateAvailable != DateTime.MinValue)
            {
                data["date_available"] = DateTimeToUnixTimestamp(request.DateAvailable);    
            }           
            data["can_relocate"] = request.CanRelocate.ToString().ToLower();
            data["notes"] = request.Notes;
            data["key_skills"] = request.KeySkills;
            data["current_employer"] = request.CurrentEmployer;
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            if (request.Owner != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();  
            }           
            data["email1"] = request.Email1;
            data["email2"] = request.Email2;
            data["web_site"] = request.WebSite;
            data["is_hot"] = request.IsHot.ToString().ToLower();
            data["desired_pay"] = request.DesiredPay;
            data["current_pay"] = request.CurrentPay;
            data["is_active"] = request.IsActive.ToString().ToLower();
            data["best_time_to_call"] = request.BestTimeToCall;
            data["password"] = request.Password;
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();       
            }           
            data["parse_resume"] = request.ParseResume.ToString().ToLower();
            data["on_duplicate"] = CamelCaseToUnderscore(Enum.GetName(typeof(OnDuplicateAction), request.OnDuplicate));

            Dictionary<string, string> files = new Dictionary<string, string>();            
            if (request.Resume != String.Empty)
            {                
                files.Add("resume", request.Resume);
            }               
            
            AddCandidateResponse response = new AddCandidateResponse(
                MakeRequest("add_candidate", data, files)
            );
            return response;
        }
        
        public AddCompanyResponse AddCompany(AddCompanyRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["name"] = request.Name;
            data["address"] = request.Address;
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            data["phone1"] = request.Phone1;
            data["phone2"] = request.Phone2;
            data["url"] = request.Url;
            data["key_technologies"] = request.KeyTechnologies;
            data["notes"] = request.Notes;
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();  
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();   
            }           
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            data["is_hot"] = request.IsHot.ToString().ToLower();
            data["fax_number"] = request.FaxNumber;
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();   
            }           
            data["on_duplicate"] = CamelCaseToUnderscore(Enum.GetName(typeof(OnDuplicateAction), request.OnDuplicate));
            
            AddCompanyResponse response = new AddCompanyResponse(MakeRequest("add_company", data));
            return response;
        }
        
        public AddContactResponse AddContact(AddContactRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["first_name"] = request.FirstName;
            data["last_name"] = request.LastName;
            data["title"] = request.Title;
            data["email1"] = request.Email1;
            data["email2"] = request.Email2;
            data["phone_work"] = request.PhoneWork;
            data["phone_cell"] = request.PhoneCell;
            data["phone_other"] = request.PhoneOther;
            data["address"] = request.Address;
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            data["is_hot"] = request.IsHot.ToString().ToLower();
            data["notes"] = request.Notes;
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            data["left_company"] = request.LeftCompany.ToString().ToLower();
            if (request.Company != -1)
            {
                data["company"] = request.Company.ToString();
            }
            if (request.ReportsTo != -1)
            {
                data["reports_to"] = request.ReportsTo.ToString();
            }
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();
            }
            data["on_duplicate"] = CamelCaseToUnderscore(Enum.GetName(typeof(OnDuplicateAction), request.OnDuplicate));
            
            AddContactResponse response = new AddContactResponse(MakeRequest("add_contact", data));
            return response;
        }
        
        public AddEmailActivityResponse AddEmailActivity(string sender, ActivityDataType dataType, int id, 
            string message, string subject, DateTime dateCreated = new DateTime(), List<string> files = null, 
            bool isResume = false)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["from"] = sender;
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(ActivityDataType), dataType));
            data["id"] = id.ToString();
            data["message"] = message;
            data["subject"] = subject;
            if (dateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(dateCreated);
            }
            data["is_resume"] = isResume.ToString().ToLower();

            Dictionary<string, string> requestFiles = new Dictionary<string, string>();
            int counter = 1;
            foreach (string file in files)
            {
                requestFiles.Add("file" + counter.ToString(), file);
                counter++;
            }

            AddEmailActivityResponse response = new AddEmailActivityResponse(
                MakeRequest("add_email_activity", data, requestFiles)
            );
            return response;
        }
        
        public AddJoborderResponse AddJoborder(AddJoborderRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();         
            if (request.Recruiter != -1)
            {
                data["recruiter"] = request.Recruiter.ToString();
            }
            if (request.Contact != -1)
            {
                data["contact"] = request.Contact.ToString();
            }
            if (request.Company != -1)
            {
                data["company"] = request.Company.ToString();
            }
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            data["client_job_id"] = request.ClientJobId;
            data["title"] = request.Title;
            data["description"] = request.Description;
            data["notes"] = request.Notes;
            switch (request.Type)
            {
                case JoborderType.Contract:
                    data["type"] = "C";
                    break;
                case JoborderType.Hire:
                    data["type"] = "H";
                    break;
                case JoborderType.ContractToHire:
                    data["type"] = "CH";
                    break;
                case JoborderType.Freelance:
                    data["type"] = "FL";
                    break;
            }
            data["duration"] = request.Duration;
            data["rate_max"] = request.RateMax;
            data["salary"] = request.Salary;
            data["status"] = request.Status;
            data["is_hot"] = request.IsHot.ToString().ToLower();
            if (request.Openings != -1)
            {
                data["openings"] = request.Openings.ToString();
            }
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            if (request.StartDate != DateTime.MinValue)
            {
                data["start_date"] = DateTimeToUnixTimestamp(request.StartDate);
            }
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.Department != -1)
            {
                data["department"] = request.Department.ToString();
            }
            if (request.Sourcer != -1)
            {
                data["sourcer"] = request.Sourcer.ToString();
            }
            if (request.OpeningsAvailable != -1)
            {
                data["openings_available"] = request.OpeningsAvailable.ToString();
            }
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();
            }
            data["on_duplicate"] = CamelCaseToUnderscore(Enum.GetName(typeof(OnDuplicateAction), request.OnDuplicate));
            
            AddJoborderResponse response = new AddJoborderResponse(MakeRequest("add_joborder", data));
            return response;
        }
        
        public AddPipelineResponse AddPipeline(int candidateId, int joborderId, string status = "", 
            bool noActivity = false, int? stars = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["candidate_id"] = candidateId.ToString();
            data["joborder_id"] = joborderId.ToString();
            data["status"] = status;
            data["no_activity"] = noActivity.ToString().ToLower();
            if (stars != null)
            {
                data["stars"] = stars.ToString();
            }
            
            AddPipelineResponse response = new AddPipelineResponse(MakeRequest("add_pipeline", data));
            return response;
        }
        
        public AddQuickNoteResponse AddQuickNote(QuickNoteDataType dataType, int id, string notes, 
            DateTime date = new DateTime())
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(QuickNoteDataType), dataType));
            data["id"] = id.ToString();
            data["notes"] = notes;
            if (date != DateTime.MinValue)
            {
                data["date"] = DateTimeToUnixTimestamp(date);
            }
            
            AddQuickNoteResponse response = new AddQuickNoteResponse(MakeRequest("add_quick_note", data));
            return response;
        }
        
        public DeleteCandidateResponse DeleteCandidate(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            
            DeleteCandidateResponse response = new DeleteCandidateResponse(MakeRequest("delete_candidate", data));
            return response;
        }
        
        public DeleteCompanyResponse DeleteCompany(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            
            DeleteCompanyResponse response = new DeleteCompanyResponse(MakeRequest("delete_company", data));
            return response;
        }
        
        public DeleteContactResponse DeleteContact(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            
            DeleteContactResponse response = new DeleteContactResponse(MakeRequest("delete_contact", data));
            return response;
        }
        
        public DeleteJoborderResponse DeleteJoborder(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            
            DeleteJoborderResponse response = new DeleteJoborderResponse(MakeRequest("delete_joborder", data));
            return response;
        }
        
        public GetActivitiesResponse GetActivities(string search = "", int rowsPerPage = 10, int pageNumber = 0, 
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));
            
            GetActivitiesResponse response = new GetActivitiesResponse(MakeRequest("get_activities", data));
            return response;
        }
        
        public GetApplicantsResponse GetApplicants(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));
            
            GetApplicantsResponse response = new GetApplicantsResponse(MakeRequest("get_applicants", data));
            return response;
        }
        
        public GetApplicationsResponse GetApplications(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            
            GetApplicationsResponse response = new GetApplicationsResponse(MakeRequest("get_applications", data));
            return response;
        }
        
        public GetAttachmentResponse GetAttachment(string guid)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["guid"] = guid;
            
            GetAttachmentResponse response = new GetAttachmentResponse(MakeRequest("get_attachment", data));
            return response;
        }
        
        public GetAttachmentsResponse GetAttachments(AttachmentDataType dataType, int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(AttachmentDataType), dataType));
            data["id"] = id.ToString();
            
            GetAttachmentsResponse response = new GetAttachmentsResponse(MakeRequest("get_attachments", data));
            return response;
        }
        
        public GetCandidateResponse GetCandidate(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            data["result"] = "extended";
            
            GetCandidateResponse response = new GetCandidateResponse(MakeRequest("get_candidate", data));
            return response;
        }
        
        public GetCandidatesResponse GetCandidates(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));
            
            GetCandidatesResponse response = new GetCandidatesResponse(MakeRequest("get_candidates", data));
            return response;
        }
        
        public GetCompaniesResponse GetCompanies(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));
            
            GetCompaniesResponse response = new GetCompaniesResponse(MakeRequest("get_companies", data));
            return response;
        }
        
        public GetCompanyResponse GetCompany(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            data["result"] = "extended";
            
            GetCompanyResponse response = new GetCompanyResponse(MakeRequest("get_company", data));
            return response;
        }
        
        public GetContactResponse GetContact(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            data["result"] = "extended";
            
            GetContactResponse response = new GetContactResponse(MakeRequest("get_contact", data));
            return response;
        }
        
        public GetContactsResponse GetContacts(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));
            
            GetContactsResponse response = new GetContactsResponse(MakeRequest("get_contacts", data));
            return response;
        }
        
        public GetJoborderResponse GetJoborder(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            data["result"] = "extended";
            
            GetJoborderResponse response = new GetJoborderResponse(MakeRequest("get_joborder", data));
            return response;
        }
        
        public GetJoborderApplicationsResponse GetJoborderApplications(int id = -1)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (id != -1)
            {
                data["id"] = id.ToString();
            }
            
            GetJoborderApplicationsResponse response = new GetJoborderApplicationsResponse(
                MakeRequest("get_joborder_applications", data)
            );
            return response;
        }
        
        public GetJobordersResponse GetJoborders(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));
            
            GetJobordersResponse response = new GetJobordersResponse(MakeRequest("get_joborders", data));
            return response;
        }
        
        public GetListsResponse GetLists(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));

            GetListsResponse response = new GetListsResponse(MakeRequest("get_lists", data));
            return response;
        }
        
        public GetMagicPreviewResponse GetMagicPreview(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            
            GetMagicPreviewResponse response = new GetMagicPreviewResponse(MakeRequest("get_magic_preview", data));
            return response;
        }
        
        public GetPipelinesResponse GetPipelines(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));         
            
            GetPipelinesResponse response = new GetPipelinesResponse(MakeRequest("get_pipelines", data));
            return response;
        }
        
        public GetPublicJobordersResponse GetPublicJoborders(string keywords = "", int maxResults = 20, int offset = 0)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["keywords"] = "";
            data["max_results"] = maxResults.ToString();
            data["offset"] = offset.ToString();
            data["result"] = "extended";
            
            GetPublicJobordersResponse response = new GetPublicJobordersResponse(
                MakeRequest("get_public_joborders", data)
            );
            return response;
        }
        
        public GetResumeResponse GetResume(AttachmentDataType dataType, int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(AttachmentDataType), dataType));
            data["id"] = id.ToString();
            
            GetResumeResponse response = new GetResumeResponse(MakeRequest("get_resume", data));
            return response;
        }
        
        public GetStatusesResponse GetStatuses(ItemDataType dataType = ItemDataType.Candidate)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(ItemDataType), dataType));
            
            GetStatusesResponse response = new GetStatusesResponse(MakeRequest("get_statuses", data));
            return response;
        }
        
        public GetTasksResponse GetTasks(string search = "", int rowsPerPage = 10, int pageNumber = 0,
            string sort = "", SortDirection sortDirection = SortDirection.Desc)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["search"] = search;
            data["rows_per_page"] = rowsPerPage.ToString();
            data["page_number"] = pageNumber.ToString();
            data["sort"] = sort;
            data["sort_direction"] = CamelCaseToUnderscore(Enum.GetName(typeof(SortDirection), sortDirection));         

            GetTasksResponse response = new GetTasksResponse(MakeRequest("get_tasks", data));
            return response;
        }

        public GetTranslationsResponse GetTranslations(List<string> strings, string language = "")
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["strings"] = JsonConvert.SerializeObject(strings);
            data["language"] = language;

            GetTranslationsResponse response = new GetTranslationsResponse(MakeRequest("get_translations", data));
            return response;
        }

        public GetUserLanguageResponse GetUserLanguage()
        {
            GetUserLanguageResponse response = new GetUserLanguageResponse(MakeRequest("get_user_language"));
            return response;
        }
        
        public SearchResponse Search(string keywords, ItemDataType? dataType = null, bool isEmail = false, 
            int maxResults = 20, int offset = 0)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["keywords"] = keywords;
            if (dataType != null)
            {
                data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(ItemDataType), dataType));             
            }
            data["is_email"] = isEmail.ToString().ToLower();
            data["max_results"] = maxResults.ToString();
            data["offset"] = offset.ToString();
            
            SearchResponse response = new SearchResponse(MakeRequest("search", data));
            return response;
        }
        
        public SetStatusResponse SetStatus(ItemDataType dataType, int id, string status, bool noActivity = false,
            string triggers = "")
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["data_type"] = CamelCaseToUnderscore(Enum.GetName(typeof(ItemDataType), dataType));
            data["id"] = id.ToString();
            data["status"] = status;
            data["no_activity"] = noActivity.ToString().ToLower();
            data["triggers"] = triggers;
            
            SetStatusResponse response = new SetStatusResponse(
                MakeRequest("set_status", data)
            );
            return response;
        }
        
        public UpdateAttachmentResponse UpdateAttachment(string guid, string file)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["guid"] = guid;

            Dictionary<string, string> files = new Dictionary<string, string>();
            files.Add("file", file);

            UpdateAttachmentResponse response = new UpdateAttachmentResponse(
                MakeRequest("update_attachment", data, files)
            );
            return response;
        }
        
        public UpdateCandidateResponse UpdateCandidate(UpdateCandidateRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = request.Id.ToString();
            data["is_resume"] = request.IsResume.ToString().ToLower();
            data["first_name"] = request.FirstName;
            data["middle_name"] = request.MiddleName;
            data["last_name"] = request.LastName;
            data["title"] = request.Title;
            data["phone_home"] = request.PhoneHome;
            data["phone_cell"] = request.PhoneCell;
            data["phone_work"] = request.PhoneWork;
            data["address"] = request.Address;
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            data["source"] = request.Source;
            if (request.DateAvailable != DateTime.MinValue)
            {
                data["date_available"] = DateTimeToUnixTimestamp(request.DateAvailable);    
            }
            if (request.CanRelocate != null)
            {
                data["can_relocate"] = request.CanRelocate.ToString().ToLower();
            }
            data["notes"] = request.Notes;
            data["key_skills"] = request.KeySkills;
            data["current_employer"] = request.CurrentEmployer;
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();
            }
            data["email1"] = request.Email1;
            data["email2"] = request.Email2;
            data["web_site"] = request.WebSite;
            if (request.IsHot != null)
            {
                data["is_hot"] = request.IsHot.ToString().ToLower();
            }
            data["desired_pay"] = request.DesiredPay;
            data["current_pay"] = request.CurrentPay;
            if (request.IsActive != null)
            {
                data["is_active"] = request.IsActive.ToString().ToLower();
            }
            data["best_time_to_call"] = request.BestTimeToCall;
            data["password"] = request.Password;
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();
            }

            Dictionary<string, string> files = new Dictionary<string, string>();            
            if (request.File != String.Empty)
            {
                files.Add("file", request.File);
            }
            
            UpdateCandidateResponse response = new UpdateCandidateResponse(
                MakeRequest("update_candidate", data, files)
            );
            return response;
        }
        
        public UpdateCompanyResponse UpdateCompany(UpdateCompanyRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = request.Id.ToString();
            data["name"] = request.Name;
            data["address"] = request.Address;
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            data["phone1"] = request.Phone1;
            data["phone2"] = request.Phone2;
            data["url"] = request.Url;
            data["key_technologies"] = request.KeyTechnologies;
            data["notes"] = request.Notes;
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            if (request.IsHot != null)
            {
                data["is_hot"] = request.IsHot.ToString().ToLower();
            }
            data["fax_number"] = request.FaxNumber;
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();
            }

            Dictionary<string, string> files = new Dictionary<string, string>();            
            if (request.File != String.Empty)
            {
                files.Add("file", request.File);
            }
            
            UpdateCompanyResponse response = new UpdateCompanyResponse(
                MakeRequest("update_company", data, files)
            );
            return response;
        }
        
        public UpdateContactResponse UpdateContact(UpdateContactRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = request.Id.ToString();
            data["first_name"] = request.FirstName;
            data["last_name"] = request.LastName;
            data["title"] = request.Title;
            data["email1"] = request.Email1;
            data["email2"] = request.Email2;
            data["phone_work"] = request.PhoneWork;
            data["phone_cell"] = request.PhoneCell;
            data["phone_other"] = request.PhoneOther;
            data["address"] = request.Address;
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            if (request.IsHot != null)
            {
                data["is_hot"] = request.IsHot.ToString().ToLower();
            }
            data["notes"] = request.Notes;
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            if (request.LeftCompany != null)
            {
                data["left_company"] = request.LeftCompany.ToString().ToLower();
            }
            if (request.Company != -1)
            {
                data["company"] = request.Company.ToString();
            }
            if (request.Department != -1)
            {
                data["department"] = request.Department.ToString();
            }
            if (request.ReportsTo != -1)
            {
                data["reports_to"] = request.ReportsTo.ToString();
            }
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();
            }

            Dictionary<string, string> files = new Dictionary<string, string>();            
            if (request.File != String.Empty)
            {
                files.Add("file", request.File);
            }
            
            UpdateContactResponse response = new UpdateContactResponse(
                MakeRequest("update_contact", data, files)
            );
            return response;
        }
        
        public UpdateJoborderResponse UpdateJoborder(UpdateJoborderRequest request)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = request.Id.ToString();
            if (request.Recruiter != -1)
            {
                data["recruiter"] = request.Recruiter.ToString();
            }
            if (request.Contact != -1)
            {
                data["contact"] = request.Contact.ToString();
            }
            if (request.Company != -1)
            {
                data["company"] = request.Company.ToString();
            }
            if (request.EnteredBy != -1)
            {
                data["entered_by"] = request.EnteredBy.ToString();
            }
            if (request.Owner != -1)
            {
                data["owner"] = request.Owner.ToString();
            }
            data["client_job_id"] = request.ClientJobId;
            data["title"] = request.Title;
            data["description"] = request.Description;
            data["notes"] = request.Notes;
            if (request.Type != null)
            {
                switch (request.Type)
                {
                    case JoborderType.Contract:
                        data["type"] = "C";
                        break;
                    case JoborderType.Hire:
                        data["type"] = "H";
                        break;
                    case JoborderType.ContractToHire:
                        data["type"] = "CH";
                        break;
                    case JoborderType.Freelance:
                        data["type"] = "FL";
                        break;
                }                           
            }
            data["duration"] = request.Duration;
            data["rate_max"] = request.RateMax;
            data["salary"] = request.Salary;
            data["status"] = request.Status;
            if (request.IsHot != null)
            {
                data["is_hot"] = request.IsHot.ToString().ToLower();
            }
            if (request.Openings != -1)
            {
                data["openings"] = request.Openings.ToString();
            }
            data["city"] = request.City;
            data["state"] = request.State;
            data["zip"] = request.Zip;
            if (request.StartDate != DateTime.MinValue)
            {
                data["start_date"] = DateTimeToUnixTimestamp(request.StartDate);
            }
            if (request.DateCreated != DateTime.MinValue)
            {
                data["date_created"] = DateTimeToUnixTimestamp(request.DateCreated);
            }
            if (request.DateModified != DateTime.MinValue)
            {
                data["date_modified"] = DateTimeToUnixTimestamp(request.DateModified);
            }
            if (request.Public != null)
            {
                data["public"] = request.Public.ToString().ToLower();
            }
            if (request.Department != -1)
            {
                data["department"] = request.Department.ToString();
            }
            if (request.Sourcer != -1)
            {
                data["sourcer"] = request.Sourcer.ToString();
            }
            if (request.OpeningsAvailable != -1)
            {
                data["openings_available"] = request.OpeningsAvailable.ToString();
            }
            if (request.Country != -1)
            {
                data["country"] = request.Country.ToString();
            }

            Dictionary<string, string> files = new Dictionary<string, string>();            
            if (request.File != String.Empty)
            {
                files.Add("file", request.File);
            }
            
            UpdateJoborderResponse response = new UpdateJoborderResponse(
                MakeRequest("update_joborder", data, files)
            );
            return response;
        }
    }
}

