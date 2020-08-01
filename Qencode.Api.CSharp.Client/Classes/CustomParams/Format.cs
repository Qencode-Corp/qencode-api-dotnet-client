using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes.CustomParams
{
    public class Format
    {
        /// <summary>
        /// Output video format. Currently supported values are mp4, webm, advanced_hls, advanced_dash. Required.
        /// </summary>
        public string output { get; set; }

        /// <summary>
        /// Output video file extension (for MP4 - defaults to '.mp4', for WEBM - defaults to '.webm').
        /// </summary>
        public string file_extension { get; set; }

        /// <summary>
        /// Output video frame size in pixels("width"x"height"). Defaults to original frame size.
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? height { get; set; }

        /// <summary>
        /// Rotate video through specified degrees value. Possible values are 90, 180, 270.
        /// </summary>
        public int? rotate { get; set; }

        /// <summary>
        /// Output video frame rate. Defaults to original frame rate.
        /// </summary>
        public string framerate { get; set; }

        /// <summary>
        /// Keyframe interval (in frames). Defaults to 90.
        /// </summary>
        public uint? keyframe { get; set; }

        /// <summary>
        /// Output video stream bitrate in kylobits.
        /// </summary>
        public uint? bitrate { get; set; }

        /// <summary>
        /// Output video stream quality (aka Constant rate factor). Use this param to produce optimized videos with variable bitrate.
        /// For H.264 the range is 0-51: where 0 is lossless and 51 is worst possible.
        /// A lower value is a higher quality and a subjectively sane range is 18-28.
        /// Consider 18 to be visually lossless or nearly so: it should look the same or nearly the same as the input but it isn't technically lossless.
        /// </summary>
        public uint? quality { get; set; }

        /// <summary>
        /// URI to store output video in. In case this value is not specified, video is temporarily stored on Qencode servers.
        /// </summary>
        public Destination destination { get; set; }

        /// <summary>
        /// Specifies the start time (in seconds) in input video to begin transcoding from.
        /// </summary>
        public double? start_time{ get; set; }

        /// <summary>
        /// Specifies duration of the video fragment (in seconds) to be transcoded.
        /// </summary>
        public double? duration { get; set; }

        /// <summary>
        /// Segment duration to split media (in seconds). Defaults to 8.
        /// </summary>
        public uint? segment_duration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string aspect_ratio { get; set; }

        /// <summary>
        /// Output video pixel format. Possible values are yuv420p, yuv422p, yuvj420p, yuvj422p. Defaults to yuv420p.
        /// </summary>
        public string pix_format { get; set; }

        /// <summary>
        /// x264 video codec settings profile. Possible values are high, main, baseline. Defaults to main.
        /// </summary>
        public string profile { get; set; }

        /// <summary>
        /// Output stream video codec. Defaults to libx264. Possible values are: libx264, libx265, libvpx, libvpx-vp9.
        /// </summary>
        public string video_codec { get; set; }

        /// <summary>
        /// Output stream video codec parameters.
        /// </summary>
        public VideoCodecParameters video_codec_parameters { get; set; }

        /// <summary>
        /// Output file audio bitrate value in kylobits. Defaults to 64.
        /// </summary>
        public uint? audio_bitrate { get; set; }

        /// <summary>
        /// Output file audio sample rate. Defaults to 44100.
        /// </summary>
        public uint? audio_sample_rate { get; set; }

        /// <summary>
        /// Output file audio channels number. Default value is 2.
        /// </summary>
        public uint? audio_channels_number { get; set; }

        /// <summary>
        /// Output file audio codec name. Possible values are: aac, vorbis. Defaults to aac.
        /// </summary>
        public string audio_codec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double? time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? interval { get; set; }

        /// <summary>
        /// Contains a list of elements each describing a single view stream.
        /// Use this for HLS or DASH outputs only. In this case you should set size, bitrate, etc. on stream level.
        /// </summary>
        public List<Stream> stream { get; set; }

        /// <summary>
        /// MPEG vs JPEG YUV range
        /// </summary>
        public string color_range { get; set; }

        /// <summary>
        /// YUV colorspace type
        /// </summary>
        public string color_space { get; set; }

        /// <summary>
        /// Chromaticity coordinates of the source primaries
        /// </summary>
        public string color_primaries { get; set; }

        /// <summary>
        /// Color Transfer Characteristic
        /// </summary>
        public string color_trc { get; set; }

        /// <summary>
        /// Specifies custom file name for HLS or MPEG-DASH manifest (master playlist).
        /// </summary>
        public string playlist_name { get; set; }

        /// <summary>
        /// Frame resize mode. 
        /// Specify 'scale' in case you want to transform frame to fit output size.
        /// Specify 'crop' in case you want to preserve input video aspect ratio. 
        /// Possible values: crop, scale.Defaults to 'scale'.
        /// </summary>
        public string resize_mode { get; set; }

        /// <summary>
        /// Tag value to pass through encoding system.  
        /// </summary>
        public string user_tag { get; set; }

        /// <summary>
        /// Specifies image format for 'thumbnail' or 'thumbnails' output. Possible values: png, jpg. Defaults to 'png'. 
        /// Note: use 'quality' param along with "image_format": "jpg" to specify image quality.
        /// </summary>
        public string image_format { get; set; }

        /// <summary>
        /// Enables Per-Title Encoding mode.
        /// </summary>
        public uint? optimize_bitrate { get; set; }

        /// <summary>
        /// Adjusts best CRF predicted for each scene with the specified value in Per-Title Encoding mode
        /// </summary>
        public int? adjust_crf { get; set; }

        /// <summary>
        /// Limits the lowest CRF (quality) for Per-Title Encoding mode to the specified value
        /// </summary>
        public uint? min_crf { get; set; }

        /// <summary>
        /// Limits the highest CRF (quality) for Per-Title Encoding mode to the specified value
        /// </summary>
        public uint? max_crf { get; set; }

        /// <summary>
        /// Allows to add a watermark / logo to the video.
        /// For streaming formats like HLS or MPEG-DASH specify logo as an attribute of a stream object.
        /// </summary>
        public Logo logo { get; set; }
    }
}
