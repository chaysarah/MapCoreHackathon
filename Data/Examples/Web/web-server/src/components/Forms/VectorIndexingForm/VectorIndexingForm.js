import React, { PureComponent } from "react";
import cn from './VectorIndexingForm.module.css';
import { fieldsValidation, validationFunctions, validationMessages } from './VectorIndexingFormFields';
import Select from '../../Select/Select';
import ApplicationContext from '../../../context/applicationContext';
import config from "../../../config";
import { Dropdown } from '../../MultipleSelect';
import Loader from '../../Loader/Loader';
import infoIcon from '../../../assets/images/infoFull.svg';


export default class VectorIndexingForm extends PureComponent {
  static contextType = ApplicationContext;
  constructor(props) {
    super(props);
    this.state = {
      selectedFields: [],
      selects: {},
      propReceived: false

    };
  }

  componentDidUpdate(prevProps) {

    if (this.props.indexedFields && prevProps.indexedFields != this.props.indexedFields) {
      let selects = {};

      this.props.indexedFields.forEach((field, i) => {
        selects[i] = field;
      })
      this.setState({ selects });
    }

    if (this.props.allFields && prevProps.allFields != this.props.allFields) {
      this.setState({ propReceived: true });
    }

  }

  componentDidMount() {
    this.props.setParentHook(this.getFieldsToIndex);
  }

  getFieldsToIndex = () => {
    return {
      fields: Object.values(this.state.selects).filter(value => value),
      layerId: this.props.layerId,
      isAlreadyIndexed: this.props.indexedFields != null && this.props.indexedFields.length > 0
    }
  }


  handleFieldsDropDownChanged = (title, id, selectNum) => {
    if (this.state.selects[selectNum] != title && title != 'Select Field') {
      this.setState({
        selects: {
          ...this.state.selects,
          [selectNum]: title
        }
      })
    }
  }

  isFieldSelected = (field, selectNum) => {
    let isSelected = false;
    Object.keys(this.state.selects).forEach(num => {
      if (selectNum != num && field == this.state.selects[num]) {
        isSelected = true;
      }
    })

    return isSelected;
  }

  getFieldsSelect(name, selectNum) {
    const fieldsList = this.props.allFields.filter(field => !this.isFieldSelected(field.name, selectNum))
      .map(field => {
        return {
          id: field.name,
          title: field.name,
          key: field.name,
          selected: this.state.selects[selectNum] == field.name
        }
      });
    const dropDownData = {
      label: 'Select ' + name + ' Search Index',
      searchable: ["Search Field", "No matching values"],
      title: this.state.selects[selectNum],
      defaultTitle: 'Select Field',
      list: fieldsList,
      onChange: (title, id) => this.handleFieldsDropDownChanged(title, id, selectNum),
    }


    return (
      <div className={cn.Row} >
        <Dropdown {...dropDownData} />
      </div>
    )
  }

  render() {
    if (!this.state.propReceived) {
      return <Loader />;
    }

    return (
      <div className={cn.Wrapper}>

        <div className={cn.InfoWrapper}>
          <img className={cn.Icon} src={infoIcon} />
          Select Up to 3 Search indexes
        </div>
        {this.getFieldsSelect('1st', 0)}
        {this.getFieldsSelect('2nd', 1)}
        {this.getFieldsSelect('3rd', 2)}
      </div>
    );
  }

}
