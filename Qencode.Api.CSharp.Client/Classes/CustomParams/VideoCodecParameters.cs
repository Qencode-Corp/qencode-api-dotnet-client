using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes.CustomParams
{
    public abstract class VideoCodecParameters
    {

    }

    public class Libx264_VideoCodecParameters : VideoCodecParameters
    {
        /// <summary>
        /// x264 video codec settings profile. Possible values are high, main, baseline. Defaults to main.
        /// </summary>
        public string vprofile { get; set; }

        /// <summary>
        /// Set of constraints that indicate a degree of required decoder performance for a profile.
        /// </summary>
        public uint level { get; set; }

        /// <summary>
        /// Context-Adaptive Binary Arithmetic Coding (CABAC) is the default entropy encoder used by x264. Possible values are 1 and 0. Defaults to 1.
        /// </summary>
        public uint coder { get; set; }

        /// <summary>
        /// Allows B-frames to be kept as references. Possible values are +bpyramid, +wpred, +mixed_refs, +dct8×8, -fastpskip/+fastpskip, +aud Defaults to None.
        /// </summary>
        public string flags2 { get; set; }

        /// <summary>
        /// One of x264's most useful features is the ability to choose among many combinations of inter and intra partitions.
        /// Possible values are +partp8x8, +partp4x4, +partb8x8, +parti8x8, +parti4x4.Defaults to None.
        /// </summary>
        public string partitions { get; set; }


        public string bf { get; set; }

        /// <summary>
        /// Defines motion detection type: 0 -- none, 1 -- spatial, 2 -- temporal, 3 -- auto. Defaults to 1.
        /// </summary>
        public uint directpred { get; set; }

        /// <summary>
        /// Motion Estimation method used in encoding.Possible values are epzs, hex, umh, full. Defaults to None.
        /// </summary>
        public string me_method { get; set; }
    }
}
