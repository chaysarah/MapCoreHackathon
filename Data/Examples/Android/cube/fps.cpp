#include "fps.h"

FPS::FPS() : mFrame(0), mFps(0)
{
    mClock.start();
}

void FPS::update()
{
    if(mClock.elapsed() / 1000.0 >= 1.f)
    {
        mFps = mFrame;
        mFrame = 0;
        mClock.restart();
    }

    ++mFrame;
}

