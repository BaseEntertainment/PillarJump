#import <AVFoundation/AVFoundation.h>

extern "C" {
    void ForcePlayAudioEvenInSilentMode() {
        NSError *error = nil;
        [[AVAudioSession sharedInstance] setCategory:AVAudioSessionCategoryPlayback error:&error];
        [[AVAudioSession sharedInstance] setActive:YES error:&error];
    }
}