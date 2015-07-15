using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFM.Common;

namespace WFM.Attribute
{
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public MultiButtonAttribute(string name)
        {
            this.Name = name;
        }
        public override bool IsValidName(ControllerContext controllerContext,
            string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }
            return controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name);
        }
    }

    public class MultiActionAttribute : ActionNameSelectorAttribute
    {
        public string Name { get; set; }
        public MultiActionAttribute(string name)
        {
            this.Name = name;
        }
        public override bool IsValidName(ControllerContext controllerContext,
            string actionName, System.Reflection.MethodInfo methodInfo)
        {
            bool result = false;
            if (string.IsNullOrEmpty(this.Name))
            {
                return false;
            }

            //同じ名前のものを優先
            if (controllerContext.HttpContext.Request.Form.AllKeys.Contains(this.Name))
                return true;

            List<String> actions = this.Name.Split(AppConst.Const_SPLIT_DELIMITER.ToCharArray()).ToList(); ;
            foreach (string action in actions)
            {
                result = controllerContext.HttpContext.Request.Form.AllKeys.Contains(action);
                if (result)
                    break;
            }

            return result;
        }
    }

}