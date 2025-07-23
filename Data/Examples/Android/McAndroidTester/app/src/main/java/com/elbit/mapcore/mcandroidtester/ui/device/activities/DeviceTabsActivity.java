package com.elbit.mapcore.mcandroidtester.ui.device.activities;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.fragment.app.FragmentStatePagerAdapter;
import androidx.viewpager.widget.PagerAdapter;
import androidx.viewpager.widget.ViewPager;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Toast;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTMapDevice;
import com.elbit.mapcore.mcandroidtester.ui.device.fragments.CreateDeviceFragment;
import com.elbit.mapcore.mcandroidtester.ui.device.fragments.LocalCacheFragment;
import com.elbit.mapcore.mcandroidtester.ui.device.fragments.OperationsFragment;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Funcs;

import com.elbit.mapcore.Interfaces.Map.IMcMapDevice;

public class DeviceTabsActivity extends AppCompatActivity implements CreateDeviceFragment.OnFragmentInteractionListener, OperationsFragment.OnFragmentInteractionListener, LocalCacheFragment.OnFragmentInteractionListener {

    private static final int PICKFILE_RESULT_CODE = 1;
    /**
     * The {@link PagerAdapter} that will provide
     * fragments for each of the sections. We use a
     * {@link FragmentPagerAdapter} derivative, which will keep every
     * loaded fragment in memory. If this becomes too memory intensive, it
     * may be best to switch to a
     * {@link FragmentStatePagerAdapter}.
     */
    private SectionsPagerAdapter mSectionsPagerAdapter;
    private IMcMapDevice mDevice;
	private CreateDeviceFragment mCreateDeviceFragment;
	
    /**
     * The {@link ViewPager} that will host the section contents.
     */
    private ViewPager mViewPager;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_device_tabs);

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());
        mViewPager = (ViewPager) findViewById(R.id.container);
        mViewPager.setAdapter(mSectionsPagerAdapter);

        TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(mViewPager);

        BaseApplication.setCurrActivityContext(this);
    }
    public Fragment findFragmentByPosition(int position) {
        FragmentPagerAdapter fragmentPagerAdapter = mSectionsPagerAdapter;
        return getSupportFragmentManager().findFragmentByTag(
                "android:switcher:" + mViewPager.getId() + ":"
                        + fragmentPagerAdapter.getItemId(position));
    }

   /* @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_device_tabs, menu);
        return true;
    }*/

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onFragmentInteraction(Uri uri) {

    }

    public void createDevice(View view) {
        view.requestFocus();
        mCreateDeviceFragment.SaveData();
        mDevice = AMCTMapDevice.getInstance().CreateDevice();
        if(mDevice !=null) {
            Toast.makeText(this, "device was created successfully", Toast.LENGTH_LONG).show();
            //disable create device tab
            Funcs.enableDisableViewGroup((ViewGroup)view.getParent(),false);
            onBackPressed();
        }
        else
            Toast.makeText(this,"error while creating device",Toast.LENGTH_LONG).show();
    }




    /**
     * A {@link FragmentPagerAdapter} that returns a fragment corresponding to
     * one of the sections/tabs/pages.
     */
    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    mCreateDeviceFragment = CreateDeviceFragment .newInstance();
                    return mCreateDeviceFragment;
                case 1:
                    return LocalCacheFragment.newInstance("aa", "ss");
                case 2:
                    return OperationsFragment.newInstance("zz", "xx");
                default:
                    return CreateDeviceFragment .newInstance();
            }
        }

        @Override
        public int getCount() {
            // Show 3 total pages.
            return 3;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return getResources().getString(R.string.create_tab_title);
                case 1:
                    return getResources().getString(R.string.local_cache_tab_title);
                case 2:
                    return getResources().getString(R.string.operations_tab_title);
            }
            return null;
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

    }


}
