using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseLearner.Infrastructure
{
    public class VideoEncoder
    {
        private MediaHandler _mhandler;
        private string RootPath;
        private VideoInfo objVideoInfo;
        public VideoEncoder()
        {
            _mhandler=new MediaHandler();
            RootPath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);
            _mhandler.FFMPEGPath = HttpContext.Current.Server.MapPath("~\\MediaTools\\ffmpeg.exe");
           
        }

        public VideoInfo EncodeVideo(string guid,string filename)
        {
            objVideoInfo = new VideoInfo();
            _mhandler.InputPath = RootPath + @"tempVideoFiles\";
            _mhandler.OutputPath = RootPath + @"VideoFiles\";
            _mhandler.FileName = guid;
            _mhandler.OutputFileName = guid;
            _mhandler.OutputExtension = ".mp4";
            _mhandler.Force = "mp4";
            _mhandler.Width = 640;
            _mhandler.Height = 380;
            _mhandler.Video_Bitrate = 500;
            _mhandler.Audio_SamplingRate = 44100;
            _mhandler.Audio_Bitrate = 128;
            objVideoInfo = _mhandler.Encode_MP4();
            return objVideoInfo;
        }

    }
}