﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Core.Domains
{
    public abstract class _Base<T>
    {
        [Key]
        [Column(Order = 0)]
        public T Id { get; set; }
        //====================================================================================================
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        //====================================================================================================
        public DateTime UpdatedDate { get; set; }
        //====================================================================================================
        public bool IsDeleted { get; set; }
        //====================================================================================================
    }
    //========================================= ********************************************** =========================================
    //========================================= ********************************************** =========================================
    [AttributeUsage(AttributeTargets.Property)]
    public class TranslatableAttribute : Attribute
    {
    }
}
