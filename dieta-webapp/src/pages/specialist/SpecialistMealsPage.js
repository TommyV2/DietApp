import React from "react";
import {NavigationBar} from "../../components/NavigationBar";
import { Tab, Tabs, Container } from "react-bootstrap";
import { RecipesPage } from "./RecipesPage";
import { ProductsPage } from "./ProductsPage";


export class SpecialistMealsPage extends React.Component {
    render(){
        return(
            <div>
                <NavigationBar type="specialist" />
                <Container>
                    <Tabs defaultActiveKey="products" id="uncontrolled-tab-example">
                        <Tab eventKey="products" title="Produkty">
                            <ProductsPage/>
                        </Tab>
                        <Tab eventKey="recipes" title="Przepisy">
                            <RecipesPage/>
                        </Tab>
                    </Tabs>
                </Container>
            </div>
        );
    }
}