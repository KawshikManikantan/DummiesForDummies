 function cm()
 {
     var codemirrorEditor = CodeMirror(document.body, {
            lineNumbers: true,
            mode: "javascript",
            theme: "base16-dark",
            value: "Code in C:"
        });
 }