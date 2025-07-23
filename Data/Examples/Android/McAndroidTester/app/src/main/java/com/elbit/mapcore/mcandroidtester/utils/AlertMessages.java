package com.elbit.mapcore.mcandroidtester.utils;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.view.WindowManager;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.General.McErrors;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Objects;

/**
 * Created by tc97803 on 05/09/2016.
 */
public class AlertMessages {

   private static void ShowMessage(Context context,
                                  CharSequence title,
                                  CharSequence textMsg,
                                  boolean isCancel,
                                  CharSequence textPositive,
                                  final DialogInterface.OnClickListener listenerPositive,
                                  CharSequence textNegative,
                                  final DialogInterface.OnClickListener listenerNegative)
   {
       AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(context);
       // set title
       alertDialogBuilder.setTitle(title);
       // set dialog message
       alertDialogBuilder
               .setMessage(textMsg)
               .setCancelable(isCancel)
               .setPositiveButton(textPositive,listenerPositive)
               .setNegativeButton(textNegative,listenerNegative);

       // create alert dialog
       AlertDialog alertDialog = alertDialogBuilder.create();

       // show it
       alertDialog.show();
   }

    public static void ShowYesNoMessage(Context context,
                                        CharSequence title,
                                        CharSequence textMsg,
                                        final DialogInterface.OnClickListener listenerPositive,
                                        final DialogInterface.OnClickListener listenerNegative)
    {
        ShowMessage(context,title,textMsg,false,"Yes",listenerPositive,"No",listenerNegative);
    }

    public static void ShowYesNoMessage(Context context,
                                        CharSequence title,
                                        CharSequence textMsg,
                                        final DialogInterface.OnClickListener listenerPositive)
    {
        DialogInterface.OnClickListener OnCancel = new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.cancel();
            }
        };
        ShowMessage(context,title,textMsg,false,"Yes",listenerPositive,"No",OnCancel);
    }

    public static void ShowMapCoreErrorMessage(Context context,
            MapCoreException ex,
            CharSequence title) {

        final AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(context);
        // set title
        alertDialogBuilder.setTitle("MC Error" + title);
        // set dialog message
        alertDialogBuilder
                .setMessage(title + " " + ex.getMessage())
                .setCancelable(false)
                .setNeutralButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.cancel();
                    }
                });


        // show it
        ((Activity) context).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                try {
                    // create alert dialog
                    alertDialogBuilder.create().show();
                }
                catch (WindowManager.BadTokenException tokenException)
                {
                    tokenException.printStackTrace();
                }
            }
        });
    }

    public static void ShowErrorMessage(Context context, CharSequence title,CharSequence msg)
    {
        ShowMessage(context,"MC Error - " + title,msg);
    }

    public static void ShowMessage(Context context, CharSequence title, CharSequence msg)
    {
        final AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(context);
        // set title
        alertDialogBuilder.setTitle(title);
        // set dialog message
        alertDialogBuilder
                .setMessage(msg)
                .setCancelable(false)
                .setNeutralButton("OK", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        dialog.cancel();
                    }
                });

        ((Activity) context).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                // create alert dialog
                AlertDialog alertDialog = alertDialogBuilder.create();

                // show it
                alertDialog.show();
            }});
    }

    public static void ShowGenericMessage(Context context, CharSequence title,CharSequence msg)
    {
        ShowMessage(context,"Error - " + title,msg);
    }

    public static String AutoRender_FileNameLog = "autorender_log.txt";
    public static String AutoRender_FileNameSuccess = "autorender_success.txt";
    public static String AutoRender_FileNameFailed = "autorender_fail.txt";

    private static void CreateAutomationFinishFile(String testFolder, String fileName)
    {
        File file = new File(testFolder, fileName);
        try {
            file.createNewFile();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private static void KillApplication(Context context)
    {
        if (context instanceof Activity) {
            ((Activity) context).runOnUiThread(new Runnable() {
                @Override
                public void run() {

                    if(BaseApplication.getMainActivity() != null)
                        BaseApplication.getMainActivity().finishAndRemoveTask();
                    if(BaseApplication.getCurrActivityContext() != null && BaseApplication.getCurrActivityContext() instanceof Activity)
                        ((Activity) BaseApplication.getCurrActivityContext()).finishAndRemoveTask();

                    int p = android.os.Process.myPid();
                    android.os.Process.killProcess(p);

                }
            });
        }
    }

    public static void AutomationFinish(String testFolder, File logFile, Context context)  {
        CreateAutomationFinishFile(testFolder, AutoRender_FileNameSuccess);
        if(logFile.length() == 0)
           logFile.delete();

        KillApplication(context);
    }

    public static void AutomationFinish(File logFile, String testFolder, boolean isShowMsg, Context context, CharSequence title, String msg)  {
        CreateAutomationFinishFile(testFolder, AutoRender_FileNameFailed);
        AutomationError(logFile, isShowMsg, context, title, msg);

        KillApplication(context);
    }

    public static void AutomationFinish(File logFile, String testFolder,boolean isShowMsg, Context context, CharSequence title, Exception ex)   {
        if (ex instanceof MapCoreException)
            AutomationFinish(logFile, testFolder, isShowMsg ,context, title, McErrors.ErrorCodeToString(((MapCoreException) ex).getErrorCode()));
        else
            AutomationFinish(logFile, testFolder, isShowMsg,context, title, ex.getMessage());
    }

    public static void AutomationError(File logFile , boolean isShowMsg, Context context, CharSequence title, String msg) {
        try {
            if (logFile != null && logFile.exists()) {
                FileOutputStream stream = new FileOutputStream(logFile);
                try {
                    String error = title.toString() + ": " + msg;
                    stream.write(error.getBytes());
                } catch (IOException ex) {
                } finally {
                    try {
                        stream.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }

        }
            else
            {
                isShowMsg = true;
            }
    } catch(FileNotFoundException ex){
    }
        if(isShowMsg)
            ShowErrorMessage(context, title, msg);
    }

    public static void AutomationError(File logFile, boolean isShowMsg, Context context, Exception McEx, CharSequence title)
    {
        AutomationError(logFile, isShowMsg, context, title, McEx.getMessage());
    }
}
