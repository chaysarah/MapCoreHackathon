package com.elbit.mapcore.mcandroidtester.utils;

        import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.content.DialogInterface.OnKeyListener;
        import android.os.Environment;
        import android.text.Editable;
        import android.view.Gravity;
import android.view.KeyEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.ArrayAdapter;
        import android.widget.Button;
        import android.widget.EditText;
        import android.widget.LinearLayout;
import android.widget.TextView;
        import android.widget.Toast;

        import java.io.File;
        import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

public class DirectoryChooserDialog
{
    private boolean m_isNewFolderEnabled = true;
    private boolean m_EnableFileCreating = false;
    private String m_initialDirectory = "";
    private Context m_context;
    private TextView m_titleView;
    private String m_sdcardDirectory = "";

    public void enableFilesChoosing(boolean enableFileChoosing) {
        this.m_EnableFileChoosing = enableFileChoosing;
    }

    private boolean m_EnableFileChoosing =false;

    private String m_dir = "";
    private List<String> m_subdirs = null;
    private ChosenDirectoryListener m_chosenDirectoryListener = null;
    private ArrayAdapter<String> m_listAdapter = null;
    private AlertDialog m_dirsDialog;

    private List<Boolean> m_IsFolder = new ArrayList<>();
    //////////////////////////////////////////////////////
    // Callback interface for selected directory
    //////////////////////////////////////////////////////
    public interface ChosenDirectoryListener
    {
        public void onChosenDir(String chosenDir);
    }

    public DirectoryChooserDialog(Context context, ChosenDirectoryListener chosenDirectoryListener, boolean enableFileChoosing)
    {
        SetDirectoryChooserDialog(context,chosenDirectoryListener,enableFileChoosing,false);
    }

    public DirectoryChooserDialog(Context context, ChosenDirectoryListener chosenDirectoryListener, boolean enableFileChoosing, boolean enableFileCreating)
    {
        SetDirectoryChooserDialog(context,chosenDirectoryListener,enableFileChoosing,enableFileCreating);
    }

    public DirectoryChooserDialog(Context context, ChosenDirectoryListener chosenDirectoryListener, boolean enableFileChoosing,String strFolderPath) {
        this(context, chosenDirectoryListener, enableFileChoosing, strFolderPath, false);
    }

    public DirectoryChooserDialog(Context context, ChosenDirectoryListener chosenDirectoryListener, boolean enableFileChoosing,String strFolderPath, boolean enableFileCreating) {
        if (!strFolderPath.isEmpty())
            m_initialDirectory = strFolderPath;
        SetDirectoryChooserDialog(context, chosenDirectoryListener, enableFileChoosing, enableFileCreating);
    }

    private void SetDirectoryChooserDialog(Context context, ChosenDirectoryListener chosenDirectoryListener, boolean enableFileChoosing, boolean enableFileCreating) {
        m_context = context;
        m_chosenDirectoryListener = chosenDirectoryListener;
        m_EnableFileChoosing = enableFileChoosing;
        m_EnableFileCreating = enableFileCreating;
        m_sdcardDirectory = Environment.getExternalStorageDirectory().getAbsolutePath() + "/";
        try {
            m_initialDirectory = new File(m_sdcardDirectory /*m_initialDirectory*/).getCanonicalPath();
        }
        catch (IOException ioe)
        {
        }
    }

    ///////////////////////////////////////////////////////////////////////
    // setNewFolderEnabled() - enable/disable new folder button
    ///////////////////////////////////////////////////////////////////////

    public void setNewFolderEnabled(boolean isNewFolderEnabled)
    {
        m_isNewFolderEnabled = isNewFolderEnabled;
    }

    public boolean getNewFolderEnabled()
    {
        return m_isNewFolderEnabled;
    }

    ///////////////////////////////////////////////////////////////////////
    // chooseDirectory() - load directory chooser dialog for initial
    // default sdcard directory
    ///////////////////////////////////////////////////////////////////////

    public void chooseDirectory()
    {
        // Initial directory is sdcard directory
        chooseDirectory(m_initialDirectory);
    }

    ////////////////////////////////////////////////////////////////////////////////
    // chooseDirectory(String dir) - load directory chooser dialog for initial
    // input 'dir' directory
    ////////////////////////////////////////////////////////////////////////////////

    public void chooseDirectory(String dir)
    {
        File dirFile = new File(dir);
        if(m_EnableFileChoosing) {
            if (!dirFile.exists() /*|| ! dirFile.isDirectory()*/) {
                dir = m_initialDirectory;
            }
        }
        else
        if (!dirFile.exists() || ! dirFile.isDirectory()) {
            dir = m_initialDirectory;
        }
        try
        {
            dir = new File(dir).getCanonicalPath();
        }
        catch (IOException ioe)
        {
            return;
        }

        m_dir = dir + File.separator;
        m_subdirs = getDirectories(dir);

        class DirectoryOnClickListener implements DialogInterface.OnClickListener
        {
            public void onClick(DialogInterface dialog, int item)
            {
                // Navigate into the sub-directory
                /*String path =  m_dir + ((AlertDialog) dialog).getListView().getAdapter().getItem(item);
                File file = new File(path);
                */Object name = ((AlertDialog) dialog).getListView().getAdapter().getItem(item);
                m_dir +=  name ;
               /* if(file.isDirectory())
                    m_dir += name;
                else
                    m_dir += File.separator + name ;*/
                updateDirectory();
            }
        }

        AlertDialog.Builder dialogBuilder =
                createDirectoryChooserDialog(dir, m_subdirs, new DirectoryOnClickListener());

        dialogBuilder.setPositiveButton("OK", new OnClickListener()
        {
            @Override
            public void onClick(DialogInterface dialog, int which)
            {
                // Current directory chosen
                if (m_chosenDirectoryListener != null)
                {
                    // Call registered listener supplied with the chosen directory
                    m_chosenDirectoryListener.onChosenDir(m_dir);
                }
            }
        }).setNegativeButton("Cancel", null);

        m_dirsDialog = dialogBuilder.create();

        m_dirsDialog.setOnKeyListener(new OnKeyListener()
        {
            @Override
            public boolean onKey(DialogInterface dialog, int keyCode, KeyEvent event)
            {
                if (keyCode == KeyEvent.KEYCODE_BACK && event.getAction() == KeyEvent.ACTION_DOWN)
                {
                    // Back button pressed
                    if ( m_dir.equals(m_sdcardDirectory) || m_dir.equals("/"))
                    {
                        // The very top level directory, do nothing
                        return false;
                    }
                    else
                    {
                        // Navigate back to an upper directory
                        String pathParent = new File(m_dir).getParent();
                        if(pathParent.equals("/"))
                            m_dir = pathParent;
                        else
                            m_dir = pathParent + "/";
                        updateDirectory();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        });

        // Show directory chooser dialog
        m_dirsDialog.show();
    }

    private boolean createSubDir(String newDir)
    {
        File newDirFile = new File(newDir);
        if (! newDirFile.exists() )
        {
            return newDirFile.mkdir();
        }

        return false;
    }

    private boolean create(String newDir)
    {
        File newDirFile = new File(newDir);
        if (! newDirFile.exists() )
        {
            return newDirFile.mkdir();
        }

        return false;
    }

    private List<String> getDirectories(String dir) {
        List<String> dirs = new ArrayList<String>();
        List<String> files = new ArrayList<String>();
        //List<String> files = new ArrayList<String>();
        try {
            File dirFile = new File(dir);
            if (m_EnableFileChoosing) {
                if (!dirFile.exists()/* || ! dirFile.isDirectory()*/) {
                    return dirs;
                }
            } else {
                if (!dirFile.exists() || !dirFile.isDirectory()) {
                    return dirs;
                }
            }
            int index = 0;
           // m_IsFolder = new ArrayList<>(dirFile.listFiles().length);
            for (File file : dirFile.listFiles()) {
                if (file.isDirectory() ) {
                    dirs.add(file.getName() +"/");
                   // m_IsFolder.add(index, true);
                }
                else if(m_EnableFileChoosing) {
                    files.add(file.getName());
                   // m_IsFolder.add(index, false);
                }
            }


        } catch (Exception e) {
        }

        Collections.sort(dirs, new Comparator<String>() {
            public int compare(String o1, String o2) {
                return o1.compareTo(o2);
            }
        });
        Collections.sort(files, new Comparator<String>() {
            public int compare(String o1, String o2) {
                return o1.compareTo(o2);
            }
        });

        dirs.addAll(files);

        return dirs;
    }

    private AlertDialog.Builder createDirectoryChooserDialog(String title, List<String> listItems,
                                                             DialogInterface.OnClickListener onClickListener)
    {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(m_context);

        // Create custom view for AlertDialog title containing
        // current directory TextView and possible 'New folder' button.
        // Current directory TextView allows long directory path to be wrapped to multiple lines.
        LinearLayout titleLayout = new LinearLayout(m_context);
        titleLayout.setOrientation(LinearLayout.VERTICAL);

        m_titleView = new TextView(m_context);
        m_titleView.setLayoutParams(new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.WRAP_CONTENT));
        m_titleView.setTextAppearance(m_context, android.R.style.TextAppearance_Large);
        m_titleView.setTextColor( m_context.getResources().getColor(android.R.color.black) );
        m_titleView.setGravity(Gravity.CENTER_VERTICAL | Gravity.CENTER_HORIZONTAL);
        m_titleView.setText(title);

        Button newDirButton = new Button(m_context);
        newDirButton.setLayoutParams(new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.WRAP_CONTENT));
        newDirButton.setText("New folder");
        newDirButton.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                final EditText input = new EditText(m_context);

                // Show new folder name input dialog
                new AlertDialog.Builder(m_context).
                        setTitle("New folder name").
                        setView(input).setPositiveButton("OK", new DialogInterface.OnClickListener()
                {
                    public void onClick(DialogInterface dialog, int whichButton)
                    {
                        Editable newDir = input.getText();
                        String newDirName = newDir.toString();
                        // Create new directory
                        if ( createSubDir(m_dir  + newDirName) )
                        {
                            // Navigate into the new directory
                            m_dir += newDirName;
                            updateDirectory();
                        }
                        else
                        {
                            Toast.makeText(m_context, "Failed to create '" + newDirName +"' folder", Toast.LENGTH_SHORT).show();
                        }
                    }
                }).setNegativeButton("Cancel", null).show();
            }
        });

        Button newFileButton = new Button(m_context);
        newFileButton.setLayoutParams(new LayoutParams(LayoutParams.FILL_PARENT, LayoutParams.WRAP_CONTENT));
        newFileButton.setText("New file");
        newFileButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                final EditText input = new EditText(m_context);

                // Show new folder name input dialog
                new AlertDialog.Builder(m_context).
                        setTitle("New file name").
                        setView(input).setPositiveButton("OK", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int whichButton) {
                        Editable newDir = input.getText();
                        String newDirName = newDir.toString();
                        // Create new file
                        m_dir += File.separator + newDirName;
                        m_dirsDialog.getButton(DialogInterface.BUTTON_POSITIVE).performClick();
                    }
                }).setNegativeButton("Cancel", null).show();
            }
        });

        if(!m_EnableFileCreating) {
            newFileButton.setVisibility(View.GONE);
            newDirButton.setVisibility(View.GONE);
        }
        titleLayout.addView(m_titleView);
        titleLayout.addView(newDirButton);
        titleLayout.addView(newFileButton);

        dialogBuilder.setCustomTitle(titleLayout);

        m_listAdapter = createListAdapter(listItems);

        dialogBuilder.setSingleChoiceItems(m_listAdapter, -1, onClickListener);
        dialogBuilder.setCancelable(false);

        return dialogBuilder;
    }

    private void updateDirectory()
    {
        m_subdirs.clear();
        m_subdirs.addAll( getDirectories(m_dir) );
        m_titleView.setText(m_dir);

        m_listAdapter.notifyDataSetChanged();
    }

    private ArrayAdapter<String> createListAdapter(List<String> items)
    {
        return new ArrayAdapter<String>(m_context,
                android.R.layout.select_dialog_item, android.R.id.text1, items)
        {
            @Override
            public View getView(int position, View convertView,
                                ViewGroup parent)
            {
                View v = super.getView(position, convertView, parent);

                if (v instanceof TextView)
                {
                    // Enable list item (directory) text wrapping
                    TextView tv = (TextView) v;
                    tv.getLayoutParams().height = LayoutParams.WRAP_CONTENT;
                    tv.setEllipsize(null);
                }
                return v;
            }
        };
    }
}