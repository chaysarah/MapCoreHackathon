//package com.elbit.mapcore.mcandroidtester;
//
//import android.app.Application;
//import android.test.ApplicationTestCase;
//
///**
// * <a href="http://d.android.com/tools/testing/testing_android.html">Testing Fundamentals</a>
// */
//public class ApplicationTest extends ApplicationTestCase<Application> {
//    public ApplicationTest() {
//        super(Application.class);
//    }
//}

import androidx.test.core.app.ApplicationProvider;
import androidx.test.ext.junit.runners.AndroidJUnit4;
import org.junit.runner.RunWith;
import org.junit.Before;
import org.junit.Test;
import android.app.Application;

import static org.junit.Assert.*;

@RunWith(AndroidJUnit4.class)
public class ApplicationTest {

    private Application application;

    @Before
    public void setUp() {
        application = ApplicationProvider.getApplicationContext();
    }

    @Test
    public void useAppContext() {
        // Test logic
        assertEquals("com.elbit.mapcore.mcandroidtester", application.getPackageName());
    }
}

