﻿using System.Collections.Generic;

namespace ArangoDBNetStandard.GraphApi
{
    public class PutVertexQuery
    {
        public bool? WaitForSync { get; set; }

        public bool? KeepNull { get; set; }

        public bool? ReturnOld { get; set; }

        public bool? ReturnNew { get; set; }

        internal string ToQueryString()
        {
            List<string> query = new List<string>();
            if (WaitForSync != null)
            {
                query.Add("waitForSync=" + WaitForSync.ToString().ToLower());
            }
            if (KeepNull != null)
            {
                query.Add("keepNull=" + KeepNull.ToString().ToLower());
            }
            if (ReturnOld != null)
            {
                query.Add("returnOld=" + ReturnOld.ToString().ToLower());
            }
            if (ReturnNew != null)
            {
                query.Add("returnNew=" + ReturnNew.ToString().ToLower());
            }
            return string.Join("&", query);
        }
    }
}
