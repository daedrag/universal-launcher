// Modules to control application life and create native browser window
const {app, BrowserWindow, ipc} = require('electron')
const serve = require('electron-serve');

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mainWindow

// extract from environment variable to find static served directory
let loadURL
if (process.env.ELECTRON_LOADER_STATIC_DIR) {
  loadURL = serve({directory: process.env.ELECTRON_LOADER_STATIC_DIR})
}

async function createWindow () {
  // Create the browser window.
  mainWindow = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      nodeIntegration: true
    }
  })

  // and load the index.html of the app.
  if (process.env.ELECTRON_LOADER_STATIC_DIR) {
    await loadURL(mainWindow);
  } else if (process.env.ELECTRON_LOADER_LOAD_URL) {
    await mainWindow.loadURL(process.env.ELECTRON_LOADER_LOAD_URL)
  } else {
    throw new Error("Cannot find STATIC_DIR or LOAD_URL set for this app")
  }

  // Open the DevTools.
  // mainWindow.webContents.openDevTools()

  // Emitted when the window is closed.
  mainWindow.on('closed', function () {
    // Dereference the window object, usually you would store windows
    // in an array if your app supports multi windows, this is the time
    // when you should delete the corresponding element.
    mainWindow = null
  })
}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.on('ready', createWindow)

// Quit when all windows are closed.
app.on('window-all-closed', function () {
  // On macOS it is common for applications and their menu bar
  // to stay active until the user quits explicitly with Cmd + Q
  if (process.platform !== 'darwin') app.quit()
})

app.on('activate', function () {
  // On macOS it's common to re-create a window in the app when the
  // dock icon is clicked and there are no other windows open.
  if (mainWindow === null) createWindow()
})

var error = function(error) {
  debugger;
  console.error(error);
  var msg = {
    /*type : "error",
    title : "Uncaught Exception",
    buttons:["ok", "close"],*/
    width : 400
  };

  switch (typeof error) {
    case "object":
        msg.title = "Uncaught Exception: " + error.code;
        msg.message = error.message;
        break;
    case "string":
        msg.message = error;
        break;
  }
  msg.detail = "Please check the console log for more details.";

  ipc.send('electron-toaster-message', msg);
}

process.on('uncaughtException', error);

// In this file you can include the rest of your app's specific main process
// code. You can also put them in separate files and require them here.
