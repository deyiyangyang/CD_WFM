using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace WFM.Common
{
 public class CodeDescription
   {
       public string Code { get; set; }
       public string Description { get; set; }
       public string Category{get;set;}
    
       public CodeDescription(string code, string description, string category)
       {
           this.Code = code;
           this.Description = description;
           this.Category = category;
       }
   }
   public static class CodeManager
   {
       //private static CodeDescription[] codes = new CodeDescription[]
       //{
       //    new CodeDescription("M","Male","Gender"),
       //    new CodeDescription("F","Female","Gender"),
       //    new CodeDescription("S","Single","MaritalStatus"),
       //    new CodeDescription("M","Married","MaritalStatus"),
       //    new CodeDescription("CN","China","Country"),
       //    new CodeDescription("US","Unite States","Country"),
       //    new CodeDescription("UK","Britain","Country"),
       //    new CodeDescription("SG","Singapore","Country")
       //};
       public static Collection<CodeDescription> GetCodes(string category, string vTenantID, string vTenantFlag)
       {
           Collection<CodeDescription> codeCollection = new Collection<CodeDescription>();
           WFMDBDataContext db = new WFMDBDataContext();
           List<uspWFMGetShiftResult> lstShift = db.uspWFMGetShift(vTenantID, vTenantFlag).ToList();
           foreach(var item in lstShift)
           {

               codeCollection.Add(new CodeDescription(item.iShiftID.ToString(), item.vShiftName, "Shift"));
           }

           //foreach(var code in codes.Where(code=>code.Category == category))
           //{
           //    codeCollection.Add(code);
           //}
           return codeCollection;
       }
   }
}