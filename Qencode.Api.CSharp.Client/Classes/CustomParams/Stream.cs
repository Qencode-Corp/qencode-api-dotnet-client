using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes.CustomParams
{
    public class Stream
    {
        /// <summary>
        /// Output video frame size in pixels("width"x"height"). Defaults to original frame size.
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// Output stream video codec. Defaults to libx264. Possible values are: libx264, libx265, libvpx, libvpx-vp9.
        /// </summary>
        public string video_codec { get; set; }

        /// <summary>
        /// Output video stream bitrate in kylobytes. Defaults to 512.
        /// Note: don't specify bitrate unless you want constant bitrate for the video. 
        /// To create variable bitrate use quality param.
        /// </summary>
        public uint bitrate { get; set; }

        /// <summary>
        /// Output video stream quality (aka Constant rate factor). Use this param to produce optimized videos with variable bitrate.
        /// For H.264 the range is 0-51: where 0 is lossless and 51 is worst possible.
        /// A lower value is a higher quality and a subjectively sane range is 18-28.
        /// Consider 18 to be visually lossless or nearly so: it should look the same or nearly the same as the input but it isn't technically lossless.
        /// </summary>
        public uint quality { get; set; }

        /// <summary>
        /// Rotate video through specified degrees value. Possible values are 90, 180, 270.
        /// </summary>
        public uint rotate { get; set; }

        /// <summary>
        /// Output video frame rate. Defaults to original frame rate.
        /// </summary>
        public string framerate { get; set; }

        /// <summary>
        /// Output video pixel format. Possible values are yuv420p, yuv422p, yuvj420p, yuvj422p. Defaults to yuv420p.
        /// </summary>
        public string pix_format { get; set; }

        /// <summary>
        /// x264 video codec settings profile. Possible values are high, main, baseline. Defaults to main.
        /// </summary>
        public string profile { get; set; }

        /// <summary>
        /// Output stream video codec parameters.
        /// </summary>
        public VideoCodecParameters video_codec_parameters { get; set; }

        /// <summary>
        /// Keyframe period (in frames). Defaults to 90.
        /// </summary>
        public uint keyframe { get; set; }

        /// <summary>
        /// Segment duration to split media (in seconds). Defaults to 8.
        /// </summary>
        public uint segment_duration { get; set; }

        /// <summary>
        /// Specifies the start time (in seconds) in input video to begin transcoding from.
        /// </summary>
        public uint start_time { get; set; }

        /// <summary>
        /// Specifies duration of the video fragment (in seconds) to be transcoded.
        /// </summary>
        public uint duration { get; set; }

        /// <summary>
        /// Output file audio bitrate value in kylobytes. Defaults to 64.
        /// </summary>
        public uint audio_bitrate { get; set; }

        /// <summary>
        /// Output file audio sample rate. Defaults to 44100.
        /// </summary>
        public uint audio_sample_rate { get; set; }

        /// <summary>
        /// Output file audio channels number. Default value is 2.
        /// </summary>
        public uint audio_channels_number { get; set; }

        /// <summary>
        /// Output file audio codec name. Possible values are: aac, vorbis. Defaults to aac.
        /// </summary>
        public string audio_codec { get; set; }

    }
}
