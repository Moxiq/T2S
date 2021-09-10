using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2PHandler
{
    public enum Gender
    {
        UNSPECIFIED = 0,
        MALE = 1,
        FEMALE = 2
    }

    public enum Encoding
    {
        // Summary:
        //     Uncompressed 16-bit signed little-endian samples (Linear PCM). Audio content
        //     returned as LINEAR16 also contains a WAV header.
        Linear16 = 1,
        //
        // Summary:
        //     MP3 audio at 32kbps.
        Mp3 = 2,
        //
        // Summary:
        //     Opus encoded audio wrapped in an ogg container. The result will be a file which
        //     can be played natively on Android, and in browsers (at least Chrome and Firefox).
        //     The quality of the encoding is considerably higher than MP3 while using approximately
        //     the same bitrate.
        OggOpus = 3
    }
}
