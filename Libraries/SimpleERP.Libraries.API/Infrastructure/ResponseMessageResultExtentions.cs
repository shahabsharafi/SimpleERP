using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http.Results;

namespace SimpleERP.Libraries.API.Infrastructure
{
    public static class ResponseMessageResultExtentions
    {

        public static ResponseMessageResult AsInline(this ResponseMessageResult self)
        {
            string type = "inline";
            if (self.Response.Content.Headers.Contains("__isAttachmentForced")) type = "attachment";

            if (self.Response.Content.Headers.ContentDisposition == null)
                self.Response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(type);
            else
                self.Response.Content.Headers.ContentDisposition.DispositionType = type;

            return self;
        }

        public static ResponseMessageResult AsAttachment(this ResponseMessageResult self)
        {
            string type = "attachment";

            if (self.Response.Content.Headers.ContentDisposition == null)
                self.Response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(type);
            else
                self.Response.Content.Headers.ContentDisposition.DispositionType = type;

            return self;
        }

        public static ResponseMessageResult As(this ResponseMessageResult self, string contentDisposition)
        {
            if (self.Response.Content.Headers.Contains("__isAttachmentForced")) contentDisposition = "attachment";

            if (self.Response.Content.Headers.ContentDisposition == null)
                self.Response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue(contentDisposition);
            else
                self.Response.Content.Headers.ContentDisposition.DispositionType = contentDisposition;

            return self;
        }

    }
}
