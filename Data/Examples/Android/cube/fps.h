#ifndef FPS_H
#define FPS_H

#include <QTime>

class FPS
{
public:
    /// @brief Constructor with initialization.
    ///
    FPS();

    /// @brief Update the frame count.
    ///
    void update();

    /// @brief Get the current FPS count.
    /// @return FPS count.
    const unsigned int getFPS() const { return mFps; }

private:
    unsigned int mFrame;
    unsigned int mFps;
    QTime mClock;
};


#endif // FPS_H
