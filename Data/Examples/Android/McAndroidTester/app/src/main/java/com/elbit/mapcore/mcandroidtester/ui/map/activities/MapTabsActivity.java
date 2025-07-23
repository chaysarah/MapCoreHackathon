package com.elbit.mapcore.mcandroidtester.ui.map.activities;

import android.net.Uri;
import android.os.Bundle;

import com.elbit.mapcore.mcandroidtester.utils.Funcs;
import com.google.android.material.tabs.TabLayout;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentPagerAdapter;
import androidx.viewpager.widget.ViewPager;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.os.Handler;
import android.view.MenuItem;

import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.model.AMCTViewPort;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.GridCoordinateSysFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.LayersFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.MapSettingsFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.OverlayManagerFragment;
import com.elbit.mapcore.mcandroidtester.ui.map.fragments.TerrainsFragment;
import com.elbit.mapcore.mcandroidtester.utils.BaseApplication;
import com.elbit.mapcore.mcandroidtester.utils.Consts;

import com.elbit.mapcore.Interfaces.Map.IMcMapViewport;

public class MapTabsActivity extends AppCompatActivity implements GridCoordinateSysFragment.OnFragmentInteractionListener, MapSettingsFragment.OnFragmentInteractionListener,TerrainsFragment.OnFragmentInteractionListener,LayersFragment.OnFragmentInteractionListener,OverlayManagerFragment.OnFragmentInteractionListener{

    private SectionsPagerAdapter mSectionsPagerAdapter;
    public IMcMapViewport.SCreateData mSCreateData;
    private ViewPager mViewPager;
    Handler handler = new Handler();

    public Handler getHandler() {return  handler;}

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_map);
        initTabs();
        //remove current viewport data
        AMCTViewPort.getViewportInCreation().resetCurViewPort();
        BaseApplication.setCurrActivityContext(this);

        handler.post(Funcs.getPerformPendingCalculationsRunnable(handler, this));
    }


    @Override
    protected void onDestroy() {
        super.onDestroy();

        // Stop the timer when the activity is destroyed
        Funcs.removeCallbacks(handler);
    }

    private void initTabs() {
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        mSectionsPagerAdapter = new SectionsPagerAdapter(getSupportFragmentManager());

        mViewPager = (ViewPager) findViewById(R.id.container);
        mViewPager.setAdapter(mSectionsPagerAdapter);
        mViewPager.setOffscreenPageLimit(4);
        TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(mViewPager);

    }


/*    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_create_map, menu);
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

    public class SectionsPagerAdapter extends FragmentPagerAdapter {

        public SectionsPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            switch (position) {
                case 0:
                    return GridCoordinateSysFragment.newInstance(Consts.VIEWPORT_COORDINATE_SYS);
                case 1:
                    return TerrainsFragment.newInstance(true);
                case 2:
                    return OverlayManagerFragment.newInstance();
                case 3:
                return MapSettingsFragment.newInstance();
                default:
                    return null;
            }
        }

        @Override
        public int getCount() {
            return 4;
        }

        @Override
        public CharSequence getPageTitle(int position) {
            switch (position) {
                case 0:
                    return getResources().getString(R.string.create_grid_coordinate_title);
                case 1:
                    return getResources().getString(R.string.create_terrains_title);
                case 2:
                    return getResources().getString(R.string.overlay_manager_title);
                case 3:
                    return getResources().getString(R.string.settings_title);
            }
            return null;
        }
    }
}
