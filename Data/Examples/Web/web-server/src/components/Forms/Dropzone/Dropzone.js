import React, { Component } from 'react';
import classNames from './Dropzone.module.css';
import { instructionMessages } from '../Dropzone/Validations';
import errorIcon from '../../../assets/images/error2.svg';

class Dropzone extends Component {

  constructor(props) {
    super(props);
    this.state = {
      isFileHover: false,
      instructionMessage: instructionMessages.default,
      isUploading: false
    };

    this.fileInputRef = React.createRef();

    this.openFileDialog = this.openFileDialog.bind(this);
    this.onFilesAdded = this.onFilesAdded.bind(this);
    this.onDragOver = this.onDragOver.bind(this);
    this.onDragLeave = this.onDragLeave.bind(this);
    this.onDrop = this.onDrop.bind(this);
    this.onSelectFile = this.onSelectFile.bind(this);

  }

  async getFile(fileEntry) {
    try {
      return await new Promise((resolve, reject) => fileEntry.file(resolve, reject));
    } catch (err) {
      console.log(err);
    }
  }

  openFileDialog() {
    if (this.props.disabled) return;
    this.fileInputRef.current.click();
    // this.setState({ isFileHover: true, instructionMessage: instructionMessages.hover });
  }

  onSelectFile() {
    this.setState({ isFileHover: true, instructionMessage: instructionMessages.hover });

  }

  // Drop handler function to get all files
  async getAllFileEntries(dataTransferItemList) {
    let fileEntries = [];
    // Use BFS to traverse entire directory/file structure
    let queue = [];

    // Unfortunately dataTransferItemList is not iterable i.e. no forEach
    for (let i = 0; i < dataTransferItemList.length; i++) {
      queue.push(dataTransferItemList[i].webkitGetAsEntry());
    }

    while (queue.length > 0) {
      let entry = queue.shift();
      if (entry.isFile) {
        const file = await this.getFile(entry);

        const fileFullPath = entry.fullPath.startsWith('/') ? entry.fullPath.substring(1) : entry.fullPath;
        file.fileFullPath = fileFullPath;

        fileEntries.push(file);
      } else if (entry.isDirectory) {
        let reader = entry.createReader();
        queue.push(...await this.readAllDirectoryEntries(reader));
      }
    }
    return fileEntries;
  }

  // Get all the entries (files or sub-directories) in a directory by calling readEntries until it returns empty array
  async readAllDirectoryEntries(directoryReader) {
    let entries = [];
    let readEntries = await this.readEntriesPromise(directoryReader);
    while (readEntries.length > 0) {
      entries.push(...readEntries);
      readEntries = await this.readEntriesPromise(directoryReader);
    }
    return entries;
  }

  // Wrap readEntries in a promise to make working with readEntries easier
  async readEntriesPromise(directoryReader) {
    try {
      return await new Promise((resolve, reject) => {
        directoryReader.readEntries(resolve, reject);
      });
    } catch (err) {
      console.log(err);
    }
  }

  onDragOver(evt) {
    evt.preventDefault();
    evt.stopPropagation();
    if (this.props.disabled) return;

    this.setState({ isFileHover: true, instructionMessage: instructionMessages.hover });

  }

  onDragLeave(e) {
    if (e.currentTarget.contains(e.relatedTarget)) {
      return;
    }
    this.setState({ isFileHover: false, instructionMessage: instructionMessages.default });
  }


  isValidFolder(dataTransferItemList) {
    return dataTransferItemList &&
      dataTransferItemList.length === 1
    // &&dataTransferItemList[0].webkitGetAsEntry().isDirectory
  }

  async onDrop(event) {
    event.preventDefault();
    const items = event.dataTransfer.items;

    if (this.isValidFolder(items)) {
      let files = await this.getAllFileEntries(items);
      this.props.onFolderSelected(files);
    } else {
      this.setState({ isFileHover: false, instructionMessage: 'Please drop a folder' })
    }
  }

  onFilesAdded(evt) {
    const files = evt.target.files;
    this.props.onFolderSelected(files);
    this.setState({ isFileHover: false });
  }

  getHiddenInputElement() {
    let isAccept = this.props.layerType == "MC Package" ? '.mcpkg' : '';
    let isWebkitdirectory = this.props.layerType != "MC Package" ? 'true' : false;
    return (
      <input
        ref={this.fileInputRef}
        className={classNames.FileInput}
        type='file'
        accept={isAccept}
        webkitdirectory={isWebkitdirectory}
        onChange={this.onFilesAdded}
        onBeforeInput={this.onSelectFile}
      />
    );
  }

  isHasError() {
    const { instructionMessage } = this.state;

    if (instructionMessage === instructionMessages.none ||
      instructionMessage === instructionMessages.default ||
      instructionMessage === instructionMessages.hover) {
      return false;
    }
    return true;
  }

  isLoader() {
    const { instructionMessage } = this.state;

    if (instructionMessage === instructionMessages.hover) {
      return true;
    }
    return false;
  }

  getInstructions() {
    const hasError = this.isHasError();
    const waiting = this.isLoader();
    const hasErrorClass = hasError ? ` ${classNames.Error}` : ''

    return (
      <div className={`${classNames.Instructions}${hasErrorClass}`}>
        {hasError ? <img src={errorIcon} className={classNames.InstructionsIcon} /> : null}
        {waiting ? <div className={classNames.loader} /> : null}
        {/* {this.props.layerType != "MC Package"? */}
        <span className={classNames.InstructionMessage}>{this.state.instructionMessage}</span>
        {/* : <span className={classNames.InstructionMessage}>"Drag MC Package to here or click to browse"</span> */}

        {/* } */}
      </div>
    )
  }

  renderDropZoneContent() {
    const hoverEffect = this.state.isFileHover ? ` ${classNames.FileHover}` : '';
    return (
      <div className={`${classNames.FileUpload}${hoverEffect}`}
        onDragOver={this.onDragOver}
        onDragLeave={this.onDragLeave}
        onDrop={this.onDrop}
        onClick={this.openFileDialog}>
        {this.getHiddenInputElement()}
        <div className={classNames.UploadPicture}></div>
        {this.getInstructions()}
      </div>
    )
  }

  render() {
    return this.renderDropZoneContent()
  }
}

export default Dropzone;