using NicolaParo.LightShow.Services.Abstractions;
using System.Runtime.InteropServices;

namespace NicolaParo.LightShow.Services.Dmx
{
    public class DmxAdapter : IDmxAdapter
    {
        private static class UDmxInterop
        {
            [DllImport("Lib/uDMX.dll")]
            public static extern bool Configure();

            [DllImport("Lib/uDMX.dll")]
            public static extern bool ChannelSet(int channel, byte data);

            [DllImport("Lib/uDMX.dll")]
            public static extern bool ChannelsSet(int channelCount, int channel, byte[] data);

            [DllImport("Lib/uDMX.dll")]
            public static extern bool Connected();
        }

        private byte[] channels = new byte[512];

        public byte GetChannel(int channelIndex)
        {
            EnsureValidChannelIndex(channelIndex);
            return channels[channelIndex];
        }
        public ReadOnlySpan<byte> GetChannels(int channelIndex, int length)
        {
            EnsureValidChannelIndex(channelIndex);
            EnsureValidChannelIndex(channelIndex + length);
            return channels[channelIndex..(channelIndex + length)];
        }
        public void SetChannel(int channelIndex, byte value)
        {
            EnsureValidChannelIndex(channelIndex);
            channels[channelIndex] = value;
            UDmxInterop.ChannelSet(channelIndex + 1, value);
        }
        public void SetChannels(int channelIndex, IList<byte> values)
        {
            EnsureValidChannelIndex(channelIndex);
            EnsureValidChannelIndex(channelIndex + values.Count);
            for (var i = 0; i < values.Count; i++)
                channels[i + channelIndex] = values[i];
            UDmxInterop.ChannelsSet(values.Count, channelIndex + 1, values.ToArray());
        }
        public bool IsConnected => UDmxInterop.Connected();

        private void EnsureValidChannelIndex(int channelIndex)
        {
            if (channelIndex < 0 || channelIndex >= channels.Length)
                throw new IndexOutOfRangeException();
        }
    }
}