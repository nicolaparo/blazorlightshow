namespace NicolaParo.LightShow.Services.Abstractions
{

    public interface IDmxAdapter
    {
        bool IsConnected { get; }

        byte GetChannel(int channelIndex);
        ReadOnlySpan<byte> GetChannels(int channelIndex, int length);
        void SetChannel(int channelIndex, byte value);
        void SetChannels(int channelIndex, IList<byte> values);
    }
}