using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIP
{
    static class Logger
    {
        static StreamWriter log;
        static string fileName;
        static bool enabled;
        static string stringStart;
        static string stringLap;
        static string stringStop;
        static string stringNotStarted;
        static Stopwatch stopwatch;
        static List<Pair> entries;

        static Logger()
        {
            string startTime = DateTime.Now.ToString();
            fileName = "log.txt";
            fileName = fileName.Replace("<date>", startTime).Replace(':','-').Replace(' ','_');
            log = new StreamWriter(fileName, true);
            log.WriteLine("Debugging initialized at <startTime>, relative time initalized");
            log.Flush();

            stringStart = "<id> starded @ <time>";
            stringStop = "<id> stopped @ <time> taking up <duration>";
            stringLap = "<id> lapped @ <time> resulting:  <duration> from last access";
            stringNotStarted = "<id> should have been running, but was not @ <time>";

            entries = new List<Pair>();

            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        
        /// <summary>
        /// not yet implemented
        /// </summary>
        /// <param name="enabled"></param>
        public static void toggle (ref bool enabled)
        {
            Logger.enabled = enabled;
        }

        ///TODO: move flushes to end
        public static void Start(string identifier)
        {
            /*using (StreamWriter log = new StreamWriter("log.txt", true))
            {
                log.WriteLine();
            }*/
            if (!entries.Contains(new Pair(identifier)))
            {
                entries.Add(new Pair(identifier, stopwatch.ElapsedMilliseconds));

                log.WriteLine(stringStart.
                    Replace("<id>", identifier).
                    Replace("<time>", stopwatch.ElapsedMilliseconds.ToString()));

            }
            else
            {
                log.WriteLine(identifier + "already exists @ " + stopwatch.ElapsedMilliseconds.ToString());
            }
            log.Flush();
        }
        public static void Lap(string identifier)
        {
            Lap(identifier, null);
        }

        public static void Lap(string identifier, string note)
        {
            long currentTime = stopwatch.ElapsedMilliseconds;
            Pair entry = entries.Find(i => i.id.Equals(identifier));
            string str;

            if (entry != null)
            {
                str = stringLap.
                    Replace("<id>", identifier).
                    Replace("<time>", currentTime.ToString()).
                    Replace("<duration>", (currentTime - entry.time).ToString());

            }
            else
            {
                str = stringNotStarted.
                    Replace("<id>", identifier).
                    Replace("<time>", currentTime.ToString());
            }

            if (note == null)
            {
                log.WriteLine(str);
            }
            else
            {
                log.WriteLine(str + " | " + note);
            }
            log.Flush();
        }

        public static void Stop (string identifier)
        {
            long currentTime = stopwatch.ElapsedMilliseconds;
            Pair entry = entries.Find(i => i.id.Equals(identifier));
            if (entry != null)
            {
                entries.Remove(new Pair(identifier));
                log.WriteLine(stringStop.
                Replace("<id>", identifier).
                Replace("<time>", currentTime.ToString()).
                Replace("<duration>", (currentTime - entry.time).ToString()));
            }
            else
            {
                log.WriteLine(stringNotStarted.
                    Replace("<id>", identifier).
                    Replace("<time>", currentTime.ToString()));
            }
            log.Flush();
        }

        
        class Pair
        {
            public Pair(string id,long time)
            {
                this.time = time;
                this.id = id;
            }

            public Pair(string id)
            {
                this.id = id;
                time = 0;
            }

            public long time;
            public string id;

            public override bool Equals(object obj)
            {
                try {
                    return id.Equals(((Pair)obj).id);
                }
                catch
                {
                    return false;
                }
            }
        }
        
    }
}
