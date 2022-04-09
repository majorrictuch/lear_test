using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Tasks
{
    public class CronUtil
    {


        public static string translateToChinese(string cronStr)
        {
            if (string.IsNullOrEmpty(cronStr))
            {
                return "cron表达式为空";
            }

            string[] cronArray = cronStr.Split(" ");
            // 表达式到年会有7段， 至少6段
            if (cronArray.Length != 6 && cronArray.Length != 7)
            {
                throw new Exception("cron表达式格式错误");
            }

            string secondCron = cronArray[0];
            string minuteCron = cronArray[1];
            string hourCron = cronArray[2];
            string dayCron = cronArray[3];
            string monthCron = cronArray[4];
            string weekCron = cronArray[5];
            string yearCron = cronArray.Length == 6 ? "" : cronArray[6];

            StringBuilder result = new StringBuilder();
            // 解析月
            if (!yearCron.Equals("*") && !yearCron.Equals("?"))
            {
                if (yearCron.Contains("/"))
                {
                    result.Append("从")
                            .Append(yearCron.Split("/")[0])
                            .Append("年开始")
                            .Append(",每")
                            .Append(yearCron.Split("/")[1])
                            .Append("年");
                }
                else if (yearCron != "")
                {
                    //  result.Append("每年").Append(monthCron).Append("月");
                    result.Append(yearCron).Append("年");
                }
                else
                {
                    //  result.Append("每年").Append(monthCron).Append("月");
                    //result.Append(yearCron).Append("年");
                }
            }
            else
            {
                if (yearCron.Equals("*"))
                {
                    result.Append("每年");
                }

            }

            if (!monthCron.Equals("*") && !monthCron.Equals("?"))
            {
                if (monthCron.Contains("/"))
                {
                    result.Append("从")
                            .Append(monthCron.Split("/")[0])
                            .Append("号开始")
                            .Append(",每")
                            .Append(monthCron.Split("/")[1])
                            .Append("月");
                }
                else if (monthCron != "")
                {
                    //  result.Append("每年").Append(monthCron).Append("月");
                    result.Append(monthCron).Append("月");
                }
                
            }
            else
            {
                if (monthCron.Equals("*"))
                {
                    result.Append("每月");
                }

            }

            // 解析周
            bool hasWeekCron = false;
            if (!weekCron.Equals("*") && !weekCron.Equals("?"))
            {
                hasWeekCron = true;
                result.Append("每周");
                string[] tmpArray = weekCron.Split(",");

                foreach (var item in tmpArray)
                {
                    switch (item)
                    {
                        case "SUN":
                            result.Append("日");
                            continue;
                        case "MON":
                            result.Append("一");
                            continue;
                        case "TUE":
                            result.Append("二");
                            continue;
                        case "WED":
                            result.Append("三");
                            continue;
                        case "THU":
                            result.Append("四");
                            continue;
                        case "FRI":
                            result.Append("五");
                            continue;
                        case "SAT":
                            result.Append("六");
                            continue;
                        default:
                            result.Append(item);
                            continue;
                    }
                }

            }
            else
            {
                if (weekCron.Equals("*"))
                {
                    result.Append("每周");
                }

            }
            //   }

            // 解析日
            if (!dayCron.Equals("?") && !"*".Equals(dayCron))
            {
                if (hasWeekCron)
                {
                    return "表达式错误，不允许同时存在指定日和指定星期";
                }
                if (dayCron.Contains("/"))
                {
                    result.Append("每月从第")
                            .Append(dayCron.Split("/")[0])
                            .Append("天开始")
                            .Append(",每")
                            .Append(dayCron.Split("/")[1])
                            .Append("天");
                }
                else
                {
                    result.Append("每月第").Append(dayCron).Append("天");
                }
            }
            else
            {
                if (dayCron.Equals("*"))
                {
                    result.Append("每日");
                }

            }

            // 解析时
            if (!hourCron.Equals("*"))
            {
                if (hourCron.Contains("/"))
                {
                    result.Append("从")
                            .Append(hourCron.Split("/")[0])
                            .Append("点开始")
                            .Append(",每")
                            .Append(hourCron.Split("/")[1])
                            .Append("小时");
                }
                else
                {
                    if ((result.ToString().Length > 0))
                    {
                        result.Append("每天").Append(hourCron).Append("点");
                    }
                }
            }
            else
            {

                result.Append("每小时");

            }

            // 解析分
            if (!minuteCron.Equals("*") && !minuteCron.Equals("0"))
            {
                if (minuteCron.Contains("/"))
                {
                    result.Append("从第")
                            .Append(minuteCron.Split("/")[0])
                            .Append("分开始").Append(",每")
                            .Append(minuteCron.Split("/")[1])
                            .Append("分");
                }
                else
                {
                    result.Append(minuteCron).Append("分");
                }
            }
            else
            {
                if (minuteCron.Equals("*"))
                {
                    result.Append("每分");
                }

            }

            // 解析秒
            if (!secondCron.Equals("*") && !secondCron.Equals("0"))
            {
                if (secondCron.Contains("/"))
                {
                    result.Append("从第")
                            .Append(secondCron.Split("/")[0])
                            .Append("秒开始")
                            .Append(",每")
                            .Append(secondCron.Split("/")[1])
                            .Append("秒");
                }
                else
                {
                    result.Append(secondCron).Append("秒");
                }
            }
            else
            {
                if (secondCron.Equals("*"))
                {
                    result.Append("每秒");
                }

            }

            if (!string.IsNullOrWhiteSpace(result.ToString()))
            {
                result.Append("执行一次");
            }
            return result.ToString();
        }

    }

}
