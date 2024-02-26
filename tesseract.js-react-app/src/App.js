import React, { useEffect, useState } from 'react';
import { createWorker } from 'tesseract.js';
import * as PDFJS from "pdfjs-dist/build/pdf";
import * as pdfjsWorker from "pdfjs-dist/build/pdf.worker";
import './App.css';
import newfile from './input.pdf';

function App() {
  
  const doOCR = async () => {
    // const worker = await createWorker('eng');
    // const { data: { text } } = await worker.recognize(newfile);
    const searchIntervals = [
      "Nom", "Posi<on" , // Position
      "Téléphone", "Période de passa<on", "Mo<f",
      "Taches/Projets","Email"
      // { start: "Nom", end: "Position" },
    ];
    const result = await getText(newfile, searchIntervals);
    console.log("TEXT  ", result.text);
    console.log("Objet Retour  ", result.objetRetour);
    setOcr(result.text);
  };

  const getText = async (pdfUrl, searchIntervals) => {
      console.log("pdfUrl ", pdfUrl);
      const objetRetour = {};
      PDFJS.GlobalWorkerOptions.workerSrc = pdfjsWorker;
      let doc = await PDFJS.getDocument(pdfUrl); // .allXfaHtml()
      console.log("Doc ", doc);
      let pdf = await doc.promise; // get all pages text
      console.log("PDF ", pdf);
      let maxPages = pdf.numPages;
      console.log("Pages ", maxPages);
      let countPromises = []; // collecting all page promises
      let allTextArrays = [];
      for (let j = 1; j <= maxPages; j++) {
          let pagePromise = pdf.getPage(j);

          let page = await pagePromise; // add page promise
          let textContentPromise = page.getTextContent();
          let text = await textContentPromise; // return content promise
          let resultList = text.items.map(s => s.str); // value page text in arrays 
          allTextArrays.push(...resultList);
      }
     
      console.log("ArrayList ", allTextArrays);
      // TODO: Appliquer un concateneur d'element sur un intervalle défini dans resultList 
      for(let j = 0; j < searchIntervals.length; j++) {
        let newText = "";
        let firstIndex = allTextArrays.indexOf(searchIntervals[j]) + 1;
        for(let i = firstIndex; i < allTextArrays.length; i++) {
          if(searchIntervals.includes(allTextArrays[i])){
              break;
          }
          newText += allTextArrays[i];
        }
        // TODO: construction de l'objet souhaité
        switch(searchIntervals[j]){
          case "Nom": 
            objetRetour.nom = newText;
            break;
          case "Posi<on":  // Position
            objetRetour.position = newText;
            break;
          case "Téléphone":
            objetRetour.phone = newText;
            break;
          case "Période de passa<on":
            objetRetour.periode = newText;
            break;
          case "Mo<f":
            objetRetour.motif = newText;
            break;
          case "Email":
            objetRetour.email = newText;
            break;
          case "Taches/Projets":

            break;
          default:
            console.log("Hello World");
            break;
        }
      }
      
      let result = allTextArrays.join(' '); // value page text 
      console.log("RESULT ", result);
      countPromises.push(result);
      
      // Wait for all pages and join text
      let allText = await Promise.all(countPromises);
      console.log("ALL TEXT ", allText);
      return { text: allText.join(''), objetRetour: objetRetour};
      // await doc.destroy(); 

  }

  const [ocr, setOcr] = useState('Recognizing...');
  useEffect(() => {
    doOCR();
  });
  return (
    <div className="App">
      <p>{ocr}</p>
    </div>
  );
}

export default App;
