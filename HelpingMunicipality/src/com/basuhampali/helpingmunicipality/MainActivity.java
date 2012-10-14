package com.basuhampali.helpingmunicipality;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.support.v4.app.NavUtils;

public class MainActivity extends Activity {

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.home);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_main, menu);
        return true;
    }

    public void reportIssueOnClick(View view){
    	Intent goToReportIssueActivity =new Intent(MainActivity.this, ReportIssueActivity.class);
    	startActivity(goToReportIssueActivity);
    }
}
