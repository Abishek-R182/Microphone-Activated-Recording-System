using NAudio.Wave;
using System;
using System.IO;
using System.Threading;

class AutoAudioRecorder
{
    private WaveInEvent waveIn;
    private WaveFileWriter writer;
    private string outputDirectory = @"C:\Users\Public\Documents"; // Can be customized
    private string outputFilePath;
    private Timer silenceTimer;
    private bool isRecording = false;

    //  Sensitivity threshold for sound detection
    private const float SilenceThreshold = 0.02f; // Adjust this based on environment noise

    // Silence duration (in seconds) before stopping the recording
    private const int SilenceDurationSeconds = 2;

    public void StartMonitoring()
    {
        waveIn = new WaveInEvent
        {
            DeviceNumber = 0,
            WaveFormat = new WaveFormat(44100, 1) // Mono, 44.1 kHz
        };

        waveIn.DataAvailable += OnDataAvailable;
        waveIn.RecordingStopped += OnRecordingStopped;

        silenceTimer = new Timer(SilenceCallback, null, Timeout.Infinite, Timeout.Infinite);

        waveIn.StartRecording();
        Console.WriteLine("Monitoring microphone... Speak to start recording.");
    }

    private void OnDataAvailable(object sender, WaveInEventArgs e)
    {
        bool hasSound = DetectSound(e.Buffer, e.BytesRecorded);

        if (hasSound)
        {
            if (!isRecording)
                StartRecording();

            writer.Write(e.Buffer, 0, e.BytesRecorded);

           
            silenceTimer.Change(SilenceDurationSeconds * 1000, Timeout.Infinite);
        }
    }

    private void StartRecording()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        outputFilePath = Path.Combine(outputDirectory, $"Recording_{timestamp}.wav");

        writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
        isRecording = true;
        Console.WriteLine("Recording started...");
    }

    private void StopRecording()
    {
        if (isRecording)
        {
            waveIn.StopRecording();
        }
    }

    private void OnRecordingStopped(object sender, StoppedEventArgs e)
    {
        writer?.Dispose();
        writer = null;
        isRecording = false;
        Console.WriteLine($"Recording stopped. Saved at: {outputFilePath}");
    }

    private void SilenceCallback(object state)
    {
        // Triggered when silence duration exceeds threshold
        StopRecording();
    }

    private bool DetectSound(byte[] buffer, int bytesRecorded)
    {
        for (int i = 0; i < bytesRecorded; i += 2)
        {
            short sample = BitConverter.ToInt16(buffer, i);
            float amplitude = Math.Abs(sample / 32768f);
            if (amplitude > SilenceThreshold)
            {
                return true;
            }
        }
        return false;
    }

    public static void Main()
    {
        AutoAudioRecorder recorder = new AutoAudioRecorder();
        recorder.StartMonitoring();

        Console.WriteLine("Press Enter to exit the program...");
        Console.ReadLine();
    }
}
