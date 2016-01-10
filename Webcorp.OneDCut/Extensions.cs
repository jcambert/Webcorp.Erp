using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using Webcorp.lib.onedcut;
using Webcorp.Model;
using Webcorp.OneDCut.Models;
using System.Web.Mvc.Html;
namespace Webcorp.OneDCut
{
    public static class Extensions
    {
        public static ReactiveList<BeamToCut> ToBeamToCut(this CutModel model,Article beam)
        {
            var result = new ReactiveList<BeamToCut>();
            model.ToCut.ForEach(x => result.Add(new BeamToCut(x.Quantity, x.Length, beam)));
            return result;
        }
        public static Stocks ToBeamStock(this CutModel model)
        {
            var result = new Stocks();
           

            model.Stocks.ForEach(x => {
                for (int i = 0; i < x.Quantity; i++)
                {
                    result.Add(new BeamStock() { Length = x.Length });
                }
               
            });
            return result;
        }

        public static void Include(this ScriptBundle bundle,string virtDirectory,IEnumerable<string> virPathes)
        {
            foreach (var path in virPathes)
            {
                bundle.Include(string.Format("{0}/{1}.js", virtDirectory, path));
            }
        }


        /// <summary>
        /// Get value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(this HttpSessionStateBase session, string key=nameof(T),T defaultValue=default(T))
        {
            if (session[key] == null)
            {
                session.SetValue( defaultValue, key);
            }
            return (T)session[key];
        }

        public static T GetValue<T>(this HttpSessionStateBase session, string key = nameof(T), Action<T> initialize=null) where T : new()
        {
            
            if (session[key] == null)
            {
                T v = new T();
                if (initialize != null) initialize(v);
                session.SetValue(v, key);
            }
            return (T)session[key];
        }

        /// <summary>
        /// Set value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(this HttpSessionStateBase session,  T value,string key = nameof(T))
        {
            session[key] = value;
        }

        public static MvcHtmlString Spin<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var tag = new TagBuilder("div");
            var spinBtnMinus = helper.SpinButton(Sign.Minus);
            var span = new TagBuilder("span");
            var input = helper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control" } });// new TagBuilder("input");
            var spinBtnPlus = helper.SpinButton(Sign.Plus);

            tag.Attributes["class"] = "input-group bootstrap-touchspin";

            span.Attributes["class"] = "input-group-addon bootstrap-touchspin-prefix";
            span.Attributes["style"] = "display: none;";

           /* input.Attributes["type"] = "text";
            input.Attributes["value"] = "0";
            input.Attributes["class"] = "form-control";
            input.Attributes["style"] = "display: block;";*/
            
            tag.InnerHtml += spinBtnMinus;
            tag.InnerHtml += span;
            tag.InnerHtml += input.ToHtmlString();
            tag.InnerHtml += spinBtnPlus;

            var result= tag.ToString(TagRenderMode.Normal);

            return new MvcHtmlString(result);
        }

        public static MvcHtmlString SpinButton(this HtmlHelper helper,Sign sign)
        {
            var btnclass = sign == Sign.Minus ? "down" : "up";
            var btnSign = sign == Sign.Minus ? "-" : "+";
            var tag = new TagBuilder("span");
            var btn = new TagBuilder("button");

            tag.Attributes["class"] = "input-group-btn";
            btn.Attributes["class"] = "btn btn-default bootstrap-touchspin-" + btnclass;
            btn.Attributes["type"] = "button";
            btn.InnerHtml = btnSign;

            tag.InnerHtml += btn;

            return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
        }
    }

    public enum Sign
    {
        Minus,
        Plus
    }
}

/*<div class="input-group bootstrap-touchspin">
    <span class="input-group-btn">
        <button class="btn btn-default bootstrap-touchspin-down" type="button">-</button>
    </span>
    <span class="input-group-addon bootstrap-touchspin-prefix" style="display: none;"></span>
    <input id = "demo3_22" type="text" value="33" name="demo3_22" class="form-control" style="display: block;">
    <span class="input-group-addon bootstrap-touchspin-postfix" style="display: none;"></span>
    <span class="input-group-btn">
        <button class="btn btn-default bootstrap-touchspin-up" type="button">+</button>
    </span>
    </div>
    */