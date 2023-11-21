using NAudio.Wave;

class SoundManager
{
    public void CallSound(string filename, int time)
    {
        try
        {
            using (var audioFile = new AudioFileReader($"../../../Sound/{filename}.mp3"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(time);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"오디오 재생 중 오류 발생: {ex.Message}");
        }
    }
}