import React, { useEffect } from 'react';
import * as PDFJS from "pdfjs-dist/build/pdf";
import * as pdfjsWorker from "pdfjs-dist/build/pdf.worker";
import newfile from './input.pdf';

const Home = () => {

    useEffect = () => {
        console.log("Test");
        getText(newfile).then((result) => {
            alert('parse ' + result);
        })
        // make sure to catch any error
        .catch(console.error);
    }

    const getText = async (pdfUrl) => {
        console.log("pdfUrl ", pdfUrl);
        PDFJS.GlobalWorkerOptions.workerSrc = pdfjsWorker;
        let doc = await PDFJS.getDocument(pdfUrl); // .allXfaHtml()
        console.log("Doc ", doc);
        let pdf = await doc.promise; // get all pages text
        let maxPages = pdf.pdfInfo.numPages;
        let countPromises = []; // collecting all page promises
        for (let j = 1; j <= maxPages; j++) {
            let pagePromise = pdf.getPage(j);
        
            let txt = "";

            let page = await pagePromise; // add page promise
            let textContentPromise = page.getTextContent();
            let text = await textContentPromise; // return content promise
            let result = text.items.map(function (s) { return s.str; }).join(''); // value page text 
            
            countPromises.push(result);
        }
        
        // Wait for all pages and join text
        let allText = await Promise.all(countPromises);
        return allText.join('');
    }

    return (
        <div>
            Home page
        </div>
    )
}

export default Home;