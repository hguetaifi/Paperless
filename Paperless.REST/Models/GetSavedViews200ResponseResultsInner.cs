/*
 * Paperless Rest Server
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Paperless.REST.Converters;

namespace Paperless.REST
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class GetSavedViews200ResponseResultsInner 
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name="id", EmitDefaultValue=true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [Required]
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets ShowOnDashboard
        /// </summary>
        [Required]
        [DataMember(Name="show_on_dashboard", EmitDefaultValue=true)]
        public bool ShowOnDashboard { get; set; }

        /// <summary>
        /// Gets or Sets ShowInSidebar
        /// </summary>
        [Required]
        [DataMember(Name="show_in_sidebar", EmitDefaultValue=true)]
        public bool ShowInSidebar { get; set; }

        /// <summary>
        /// Gets or Sets SortField
        /// </summary>
        [Required]
        [DataMember(Name="sort_field", EmitDefaultValue=false)]
        public string SortField { get; set; }

        /// <summary>
        /// Gets or Sets SortReverse
        /// </summary>
        [Required]
        [DataMember(Name="sort_reverse", EmitDefaultValue=true)]
        public bool SortReverse { get; set; }

        /// <summary>
        /// Gets or Sets FilterRules
        /// </summary>
        [Required]
        [DataMember(Name="filter_rules", EmitDefaultValue=false)]
        public List<GetSavedViews200ResponseResultsInnerFilterRulesInner> FilterRules { get; set; }

        /// <summary>
        /// Gets or Sets Owner
        /// </summary>
        [Required]
        [DataMember(Name="owner", EmitDefaultValue=false)]
        public GetSavedViews200ResponseResultsInnerOwner Owner { get; set; }

        /// <summary>
        /// Gets or Sets UserCanChange
        /// </summary>
        [Required]
        [DataMember(Name="user_can_change", EmitDefaultValue=true)]
        public bool UserCanChange { get; set; }

    }
}
