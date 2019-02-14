﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using Piraeus.Core.Metadata;

namespace Piraeus.Module
{
    [Cmdlet(VerbsCommon.Add, "PiraeusSubscriptionMetadata")]
    public class UpsertSubscriptionMetadata : Cmdlet
    {
        [Parameter(HelpMessage = "Url of the service.", Mandatory = true)]
        public string ServiceUrl;

        [Parameter(HelpMessage = "Security token used to access the REST service.", Mandatory = true)]
        public string SecurityToken;

        [Parameter(HelpMessage = "Unique URI identifier of subscription.", Mandatory = true)]
        public string SubscriptionUriString;

        [Parameter(HelpMessage = "Security identity from claims; required for actively connected subsystems; otherwise omit.", Mandatory = false)]
        public string Identity;

        [Parameter(HelpMessage = "Optional description of the subsription.", Mandatory = false)]
        public string Description;

        [Parameter(HelpMessage = "List of key/value indexes for the subscription.", Mandatory = false)]
        public List<KeyValuePair<string, string>> Indexes;

        [Parameter(HelpMessage = "Required for passively connected subsystems; otherwise omit.", Mandatory = false)]
        public string NotifyAddress;

        [Parameter(HelpMessage = "Type of security token used for passively connected subsystem; otherwise omit.", Mandatory = false)]
        public SecurityTokenType? TokenType;

        [Parameter(HelpMessage = "Symmetric key if a passively connection subsystem that uses SWT or JWT tokens; otherwise omit.", Mandatory = false)]
        public string SymmetricKey;

        [Parameter(HelpMessage = "Expiration of the subscription.", Mandatory = false)]
        public DateTime? Expires;

        [Parameter(HelpMessage = "Time-To-Live for retained messages.", Mandatory = false)]
        public TimeSpan? TTL;

        [Parameter(HelpMessage = "The rate retained messages are sent when the subsystem reconnects.", Mandatory = false)]
        public TimeSpan? SpoolRate;

        [Parameter(HelpMessage = "Durably persist messages for the TTL when the subsystem is disconnected.", Mandatory = false)]
        public bool DurableMessaging;

        [Parameter(HelpMessage = "Optional claim type for the identity used as the cache key.  If omitted, the resource URI query string must contain cachekey parameter and value to set the key.  If query string parameter is used it will override the claim type.")]
        public string ClaimKey;


        protected override void ProcessRecord()
        {
            SubscriptionMetadata metadata = new SubscriptionMetadata()
            {
                IsEphemeral = false,
                SubscriptionUriString = this.SubscriptionUriString,
                Description = this.Description,
                Identity = this.Identity,
                Indexes = this.Indexes,
                NotifyAddress = this.NotifyAddress,
                Expires = this.Expires,
                TokenType = this.TokenType,
                TTL = this.TTL,
                SymmetricKey = this.SymmetricKey,
                SpoolRate = this.SpoolRate,
                DurableMessaging = this.DurableMessaging,
                ClaimKey = this.ClaimKey                
            };

            string url = String.Format("{0}/api/subscription/upsertsubscriptionmetadata", ServiceUrl);
            RestRequestBuilder builder = new RestRequestBuilder("PUT", url, RestConstants.ContentType.Json, false, SecurityToken);
            RestRequest request = new RestRequest(builder);

            request.Put<SubscriptionMetadata>(metadata);
        }
    }
}
