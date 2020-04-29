import React from 'react';

class Catalogs extends React.Component {

    constructor(props) {
        super(props);
    }

    renderTableData() {
        return this.props.catalogs.map((catalog) => {
            return (
                <tr key={catalog.name} onClick={() => this.props.loadTables(catalog)}>
                    <td className="text-left">{catalog.name}</td>
                </tr>
            )
        })
    }

    render() {
        return (
            <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                <thead>
                <tr>
                    <th className="text-left">Catalogs</th>
                </tr>
                </thead>
                <tbody>
                {this.renderTableData()}
                </tbody>
            </table>
        )
    }
}

export default Catalogs;
