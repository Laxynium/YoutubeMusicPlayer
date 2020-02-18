set appName=com.YoutubeMusicPlayer
set localLocation="C:\Users\Grzegorz\Desktop\app"
adb shell run-as %appName% cp -R files /sdcard/tmp && adb pull /sdcard/tmp/files %localLocation%