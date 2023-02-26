import { TestBed } from '@angular/core/testing';

import { VeiculoResolver } from './veiculo.resolver';

describe('VeiculoResolver', () => {
  let resolver: VeiculoResolver;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    resolver = TestBed.inject(VeiculoResolver);
  });

  it('should be created', () => {
    expect(resolver).toBeTruthy();
  });
});
