using System.Threading.Tasks;
using Jint;
using YoutubeMusicPlayer.AbstractLayer;

namespace YoutubeMusicPlayer.Droid.AbstractLayer
{
    public class ScriptIdEncoder: IScriptIdEncoder
    {
        public async Task<string> EncodeAsync(string scriptId)
        {
            return await Task.Run(() =>
            {
                var engine = new Engine().SetValue("SomeAppendedValue", scriptId);

                engine.Execute(@"
function i(t, e) {
        if (-1 < t.indexOf(""/"")) {
            t = t.split(""/"");
            for (var r = 0, n = """"; r < t.length; r++)
                n += String.fromCharCode(t[r]);
            return ""s"" == e ? n : parseInt(n)
        }
        return ""s"" == e ? String.fromCharCode(t) : parseInt(String.fromCharCode(t))
    }
        SomeAppendedValue = (function (SomeT) {
         for (var e = 0, r = 0, n = """"; r < SomeT.length; r++) {
            if (e = SomeT.charCodeAt(r),
            i(""54/52"", ""n"") < e && e < i(""57/49"", ""n""))
                e = e == i(""54/53"", ""n"") ? i(""57/48"", ""n"") : e - 1;
            else if (i(""57/54"", ""n"") < e && e < i(""49/50/51"", ""n""))
                e = e == i(""49/50/50"", ""n"") ? i(""57/55"", ""n"") : e + 1;
            else if (i(""52/55"", ""n"") < e && e < i(""53/51"", ""n""))
                switch (e) {
                case i(""52/56"", ""n""):
                    e = i(""53/55"", ""n"");
                    break;
                case i(""52/57"", ""n""):
                    e = i(""53/54"", ""n"");
                    break;
                case i(""53/48"", ""n""):
                    e = i(""53/53"", ""n"");
                    break;
                case i(""53/49"", ""n""):
                    e = i(""53/52"", ""n"");
                    break;
                case i(""53/50"", ""n""):
                    e = i(""53/51"", ""n"")
                }
            else
                i(""53/50"", ""n"") < e && e < i(""53/56"", ""n"") ? e = Math.round(i(e.toString()) / 2).toString().charCodeAt(0) : e == i(""52/53"", ""n"") && (e = i(""57/53"", ""n""));
            n += String.fromCharCode(e)
        }
        return n
})(SomeAppendedValue);");

                var value = engine.GetValue("SomeAppendedValue");

                return value.ToString();
            });
          
        }
    }
}