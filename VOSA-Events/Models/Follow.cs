﻿namespace VOSA_Events.Models
{
    public class Follow
    {
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        public int EventID { get; set; }
        public virtual Event Event { get; set; }
    }
}
