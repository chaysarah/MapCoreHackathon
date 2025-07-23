package com.elbit.mapcore.mcandroidtester.ui.adapters;

import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.CheckedTextView;

import com.elbit.mapcore.General.ObjectRef;
import com.elbit.mapcore.mcandroidtester.R;
import com.elbit.mapcore.mcandroidtester.utils.AlertMessages;

import java.util.List;

import com.elbit.mapcore.General.MapCoreException;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcFileFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcFont;
import com.elbit.mapcore.Interfaces.OverlayManager.IMcLogFont;
import com.elbit.mapcore.Structs.SMcFileSource;
import com.elbit.mapcore.Structs.SMcVariantLogFont;

/**
 * Created by TC97803 on 21/11/2017.
 */

public class FontsAdapter extends ArrayAdapter<IMcFont> {
    private final Context mContext;
    private int mResourceId;

    /**
     * Constructor
     *
     * @param context  The current context.
     * @param resource The resource ID for a layout file containing a TextView to use when
     *                 instantiating views.
     * @param objects  The objects to represent in the ListView.
     */
    public FontsAdapter(Context context, int resource, List<IMcFont> objects) {
        super(context, resource, objects);
        mResourceId = resource;
        mContext = context;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        convertView = ((Activity) getContext()).getLayoutInflater().inflate(mResourceId, parent, false);
        if (getCount() > 0 && (getItem(position) != null)) {
            try {
                IMcFont font = getItem(position);
                String text="";
                if(font instanceof IMcLogFont)
                {
                    IMcLogFont logFont = (IMcLogFont)font;
                    SMcVariantLogFont variantLogFont = logFont.GetLogFont();
                    String strLogParams = "IMcLogFont (";
                    strLogParams = strLogParams.concat(variantLogFont.LogFont.lfFaceName);
                    if(variantLogFont.LogFont.lfItalic > 0)
                        strLogParams = strLogParams.concat(", italic");
                    if(variantLogFont.LogFont.lfWeight > 400)
                        strLogParams = strLogParams.concat(", bold");
                    if(variantLogFont.LogFont.lfUnderline > 0)
                        strLogParams = strLogParams.concat(", underline");
                    if(variantLogFont.LogFont.lfHeight > 0)
                        strLogParams = strLogParams.concat(", font size " ).concat(String.valueOf(variantLogFont.LogFont.lfHeight));
                    if(variantLogFont.bIsUnicode)
                        strLogParams = strLogParams.concat(", unicode");
                    text = strLogParams.concat(")");
                }
                else if(font instanceof IMcFileFont)
                {
                    IMcFileFont fileFont = (IMcFileFont)font;
                    SMcFileSource fileSource = new SMcFileSource();
                    ObjectRef<Integer> height = new ObjectRef<>();
                    fileFont.GetFontFileAndHeight(fileSource,height);
                    String isMemoryBuffer = "(is memory buffer)";
                    if(!fileSource.bIsMemoryBuffer)
                        isMemoryBuffer = fileSource.strFileName;
                    text = "IMcFileFont (" +  isMemoryBuffer +")";
                }
                CheckedTextView checkedTextView = ((CheckedTextView) convertView.findViewById(R.id.list_view));
                checkedTextView.setText(text);

            } catch (MapCoreException e) {
                AlertMessages.ShowMapCoreErrorMessage(mContext, e, "GetFontFileAndHeight/GetLogFont");
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }

        }
        return convertView;
    }

}
