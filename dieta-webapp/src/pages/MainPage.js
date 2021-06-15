import React from "react";
import { NavigationBar } from "../components/NavigationBar";
import { Container, Row, CardGroup } from "react-bootstrap";
import {CustomCard} from "../components/CustomCard";
import { url } from "../commons/properties";


export class MainPage extends React.Component {

    async componentDidMount(){
        const requestOptions = {
            method: 'POST'
          }
          fetch(url + '/account/logout', requestOptions)
          .then(async response => {
                document.cookie = "customerId=; expires = Thu, 01 Jan 1970 00:00:00 GMT; path=/;";
                document.cookie = "specialistId=; expires = Thu, 01 Jan 1970 00:00:00 GMT; path=/;";
                // window.location.href = '/specialist/dashboard';
              })
              .catch(error => {
                console.log("error catched")
                console.log(error.toString());
            });
        
    }

    render() {
        return (

            <div>
                <NavigationBar type="main" />
                <Container className='mt-5'></Container>
                <Container fluid>
                    <Row className="justify-content-md-center">
                        <CardGroup>
                            <CustomCard title='Strefa klienta' to='/client' width='40rem' src='/images/1 nXOV7tKcYNK8U8b9hFIjRA.png'/>
                            <CustomCard title='Strefa specjalisty' to='/specialist' width='40rem' src='/images/091d4f1bebc76ea61eec0b9d8af26e5f.jpg'/>
                        </CardGroup>
                    </Row>
                </Container>
            </div>
        );
    }
}