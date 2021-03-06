﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles
{
    public class GenericProfile : IProfile
    {
        public int? Id
        {
            get;
            set;
        }

        public LocationAccess InLocationAccess
        {
            get;
            set;
        }

        public IEnumerable<LocationAccess> OutLocationAccess
        {
            get;
            set;
        }
        public SignContextProfile SignProfile
        {
            get;
            set;
        }

        public EnvelopeType EnvelopeType
        {
            get;
            set;
        }
        public LocationAccess BackupLocationAccess
        {
            get;
            set;
        }
        public void Validate()
        {
            if (this.InLocationAccess != null)
            {
                this.InLocationAccess.Validate();
            }
            if(this.OutLocationAccess!=null)
            {
                foreach (var outLoc in this.OutLocationAccess)
                {
                    outLoc.Validate();
                }
            }

        }     
    }
}
