package com.elbit.mapcore.mcandroidtester.model;

public class AMCTTimer {
    long start;
    long measureTime;

    public AMCTTimer() {
        start = 0;
        measureTime = 0;
    }

    public void start() {
        start = System.nanoTime();
    }

    public void stop() {
        measureTime += (System.nanoTime() - start);
    }

    public long getMeasureTime() {
        return (System.nanoTime() - start);
    }

    public void reset() {
        start = 0;
        measureTime = 0;
    }
}
